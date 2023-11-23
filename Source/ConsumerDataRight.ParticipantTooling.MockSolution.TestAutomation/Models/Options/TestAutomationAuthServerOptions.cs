namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options
{
    public class TestAutomationAuthServerOptions
    {
        public string CDRAUTHSERVER_BASEURI { get; set; }

        // Running standalone CdrAuthServer?
        public bool STANDALONE { get; set; }

        // X-TlsClientCertThumbprint header to add if running standalone
        public string XTLSCLIENTCERTTHUMBPRINT { get; set; }

        // X-TlsClientCertThumbprint (for additional certificate) header to add if running standalone            
        public string XTLSADDITIONALCLIENTCERTTHUMBPRINT { get; set; }

        public int? ACCESSTOKENLIFETIMESECONDS { get; set; }

        /// <summary>
        ///  Running CdrAuthServer in headless mode (for authentication)?
        /// </summary>
        public bool HEADLESSMODE { get; set; }

        /// <summary>
        /// Running CdrAuthServer with JARM encryption turned on?
        /// </summary>
        public bool JARM_ENCRYPTION_ON { get; set; }
    }
}
