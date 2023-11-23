using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders.Banking.Transactions
{
    public class BankingTransaction
    {
        /// <summary>
        /// ID of the account for which transactions are provided
        /// </summary>
        [JsonProperty("accountId", Required = Required.Always)]
        public string AccountId { get; set; }
        /// <summary>
        /// A unique ID of the transaction adhering to the standards for ID permanence. 
        /// This is mandatory (through hashing if necessary) unless there are specific and justifiable technical reasons why a transaction cannot be uniquely identified for a particular account type. 
        /// It is mandatory if isDetailAvailable is set to true.
        /// </summary>
        [JsonProperty("transactionId")]
        public string? TransactionId { get; set; }
        /// <summary>
        /// True if extended information is available using the transaction detail end point. False if extended data is not available.
        /// </summary>
        [JsonProperty("isDetailAvailable", Required = Required.Always)]
        public bool IsDetailAvailable { get; set; }
        /// <summary>
        /// The type of the transaction
        /// </summary>
        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }
        /// <summary>
        /// Status of the transaction whether pending or posted. 
        /// Note that there is currently no provision in the standards to guarantee the ability to correlate a pending transaction with an associated posted transaction.
        /// </summary>
        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }
        /// <summary>
        /// The transaction description as applied by the financial institution.
        /// </summary>
        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }
        /// <summary>
        /// The time the transaction was posted. 
        /// This field is Mandatory if the transaction has status POSTED. This is the time that appears on a standard statement.
        /// </summary>
        [JsonProperty("postingDateTime")]
        public string? PostingDateTime { get; set; }
        /// <summary>
        /// Date and time at which assets become available to the account owner in case of a credit entry, or cease to be available to the account owner in case of a debit transaction entry.
        /// </summary>
        [JsonProperty("valueDateTime")]
        public string? ValueDateTime { get; set; }
        /// <summary>
        /// The time the transaction was executed by the originating customer, if available.
        /// </summary>
        [JsonProperty("executionDateTime")]
        public string? ExecutionDateTime { get; set; }
        /// <summary>
        /// The value of the transaction. Negative values mean money was outgoing from the account.
        /// </summary>
        [JsonProperty("amount", Required = Required.Always)]
        public string Amount { get; set; }
        /// <summary>
        /// The currency for the transaction amount. AUD assumed if not present.
        /// </summary>
        [JsonProperty("currency")]
        public string? Currency { get; set; }
        /// <summary>
        /// The reference for the transaction provided by the originating institution. Empty string if no data provided.
        /// </summary>
        [JsonProperty("reference", Required = Required.Always)]
        public string Reference { get; set; }
        /// <summary>
        /// Name of the merchant for an outgoing payment to a merchant.
        /// </summary>
        [JsonProperty("merchantName")]
        public string? MerchantName { get; set; }
        /// <summary>
        /// The merchant category code (or MCC) for an outgoing payment to a merchant.
        /// </summary>
        [JsonProperty("merchantCategoryCode")]
        public string? MerchantCategoryCode { get; set; }
        /// <summary>
        /// BPAY Biller Code for the transaction (if available).
        /// </summary>
        [JsonProperty("billerCode")]
        public string? BillerCode { get; set; }
        /// <summary>
        /// Name of the BPAY biller for the transaction (if available)
        /// </summary>
        [JsonProperty("billerName")]
        public string? BillerName { get; set; }
        /// <summary>
        /// BPAY CRN for the transaction (if available). 
        /// Where the CRN contains sensitive information, it should be masked in line with how the Data Holder currently displays account identifiers in their existing online banking channels.
        /// If the contents of the CRN match the format of a Credit Card PAN they should be masked according to the rules applicable for MaskedPANString.
        /// If the contents are otherwise sensitive, then it should be masked using the rules applicable for the MaskedAccountString common type.
        /// </summary>
        [JsonProperty("crn")]
        public string? Crn { get; set; }
        /// <summary>
        /// 6 Digit APCA number for the initiating institution. The field is fixed-width and padded with leading zeros if applicable.
        /// </summary>
        [JsonProperty("apcaNumber")]
        public string? ApcaNumber { get; set; }
    }
}
