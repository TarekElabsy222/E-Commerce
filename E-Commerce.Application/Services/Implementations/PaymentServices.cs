using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.CartDto;
using E_Commerce.Application.DTOs.PayMentDto;
using E_Commerce.Application.DTOs.Review;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;
using Stripe;


namespace E_Commerce.Application.Services.Implementations
{
    public class PaymentServices : IPaymentServices
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor
        public PaymentServices(IMapper mapper, IUnitOfWork unitOfWork, IOptions<StripeSettings> options)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Handle funtions       
        public async Task<ServiceResponse> CreateCheckoutSession(CreatePayment dto, ApplicationUser user)
        {
            var newPayment = await CreatePaymentAsync(dto, user);

            if (newPayment is null) return new ServiceResponse(Success: false, Message: "Failed to create payment.");               


            if (!IsValidCurrency(newPayment.Currency)) return  new ServiceResponse(Success: false, Message: "Invalid currency.");


            var paymentIntentOptions = new PaymentIntentCreateOptions
            {
                Amount = (long)(newPayment.Amount * 100),
                Currency = newPayment.Currency.ToLower(),
                Description = newPayment.Description,

                PaymentMethodTypes = new List<string>
                {
                   newPayment.Method
                },

                Metadata = new Dictionary<string, string>
                {
                   { "UserId", user.Id.ToString() },
                   { "PaymentId", newPayment.Id.ToString() }
                }
            };

            var paymentService = new PaymentIntentService();

            var paymentIntent = await paymentService.CreateAsync(paymentIntentOptions);

            return new ServiceResponse(Success: true, Message: "Payment created successfully.");
           
        }

        public async Task<ServiceResponse> DeletePayment(Guid id)
        {
            if (id == Guid.Empty)
                return new ServiceResponse(false, "Invalid payment id");

            var payment = await _unitOfWork.Payments.GetSingleAsync(p => p.Id == id);
            if (payment is null) return new ServiceResponse(false, "Cart not found");
            await _unitOfWork.Payments.DeleteAsync(id);
            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Payment deleted successfully!")
                : new ServiceResponse(false, "Failed to delete payment");
        }

        public async Task<IEnumerable<GetPayment>> GetAllPayments()
        {
            var payments = await _unitOfWork.Payments.GetAllAsync();
            if (payments is null) return [];
            return _mapper.Map<List<GetPayment>>(payments);

        }

        public async Task<GetPayment> GetPayment(Guid id)
        {
            var rawData = await _unitOfWork.Payments.GetSingleAsync(c => c.Id == id);
            return rawData is null ? null! : _mapper.Map<GetPayment>(rawData);
        }



        private async Task<Payment?> CreatePaymentAsync(CreatePayment dto, ApplicationUser user)
        {
            var payment = _mapper.Map<Payment>(dto);

            payment.CustomerId = user.Id;
            payment.Customer = user;
            payment.Method = "card";

            await _unitOfWork.Payments.AddAsync(payment);

            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? payment : null;
        }

        private bool IsValidCurrency(string currency)
        {
            return SupportedCurrencies.Contains(currency.ToLower());
        }

        private readonly HashSet<string> SupportedCurrencies = new HashSet<string>
        {
            "usd", "aed", "afn", "all", "amd", "ang", "aoa", "ars", "aud", "awg", "azn", "bam",
            "bbd", "bdt", "bgn", "bhd", "bif", "bmd", "bnd", "bob", "brl", "bsd", "bwp", "byn",
            "bzd", "cad", "cdf", "chf", "clp", "cny", "cop", "crc", "cve", "czk", "djf", "dkk",
            "dop", "dzd", "egp", "etb", "eur", "fjd", "fkp", "gbp", "gel", "gip", "gmd", "gnf",
            "gtq", "gyd", "hkd", "hnl", "hrk", "htg", "huf", "idr", "ils", "inr", "isk", "jmd",
            "jod", "jpy", "kes", "kgs", "khr", "kmf", "krw", "kwd", "kyd", "kzt", "lak", "lbp",
            "lkr", "lrd", "lsl", "mad", "mdl", "mga", "mkd", "mmk", "mnt", "mop", "mur", "mvr",
            "mwk", "mxn", "myr", "mzn", "nad", "ngn", "nio", "nok", "npr", "nzd", "omr", "pab",
            "pen", "pgk", "php", "pkr", "pln", "pyg", "qar", "ron", "rsd", "rub", "rwf", "sar",
            "sbd", "scr", "sek", "sgd", "shp", "sle", "sos", "srd", "std", "szl", "thb", "tjs",
            "tnd", "top", "try", "ttd", "twd", "tzs", "uah", "ugx", "uyu", "uzs", "vnd", "vuv",
            "wst", "xaf", "xcd", "xof", "xpf", "yer", "zar", "zmw", "usdc", "btn", "ghs", "eek",
            "lvl", "svc", "vef", "ltl", "sll", "mro"
        };
        #endregion
    }
}
