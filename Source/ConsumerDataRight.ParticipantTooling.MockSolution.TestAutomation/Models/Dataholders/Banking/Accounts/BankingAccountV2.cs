using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders.Banking.Accounts
{

    public class BankingAccountV2
    {
        /// <summary>
        /// A unique ID of the account adhering to the standards for ID permanence.
        /// </summary>
        [JsonProperty("accountId", Required = Required.Always)]
        public string AccountId { get; set; }
        /// <summary>
        /// Date that the account was created (if known).
        /// </summary>
        [JsonProperty("creationDate")]
        public string? CreationDate { get; set; }
        /// <summary>
        /// The display name of the account as defined by the bank. This should not incorporate account numbers or PANs. 
        /// If it does the values should be masked according to the rules of the MaskedAccountString common type.
        /// </summary>
        [JsonProperty("displayName", Required = Required.Always)]
        public string DisplayName { get; set; }
        /// <summary>
        /// A customer supplied nick name for the account.
        /// </summary>
        [JsonProperty("nickname")]
        public string? Nickname { get; set; }
        /// <summary>
        /// Open or closed status for the account. If not present then OPEN is assumed.
        /// </summary>
        [JsonProperty("openStatus")]
        public string? OpenStatus { get; set; }
        /// <summary>
        /// Flag indicating that the customer associated with the authorisation is an owner of the account. Does not indicate sole ownership, however. If not present then 'true' is assumed.
        /// </summary>
        [JsonProperty("isOwned")]
        public bool? IsOwned { get; set; }
        /// <summary>
        /// Value indicating the number of customers that have ownership of the account, according to the data holder's definition of account ownership.
        /// Does not indicate that all account owners are eligible consumers.
        /// </summary>
        [JsonProperty("accountOwnership")] 
        public string AccountOwnership { get; set; } 
        /// <summary>
        /// A masked version of the account. Whether BSB/Account Number, Credit Card PAN or another number
        /// </summary>
        [JsonProperty("maskedNumber", Required = Required.Always)]
        public string MaskedNumber { get; set; }
        /// <summary>
        /// The category to which a product or account belongs.
        /// </summary>
        [JsonProperty("productCategory", Required = Required.Always)]
        public string ProductCategory { get; set; }
        /// <summary>
        /// The unique identifier of the account as defined by the data holder (akin to model number for the account).
        /// </summary>
        [JsonProperty("productName", Required = Required.Always)]
        public string ProductName { get; set; }
    }
}
