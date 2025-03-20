namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
    using Dapper;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Options;
    using Serilog;

    public class SqlQueryService : ISqlQueryService
    {
        private readonly TestAutomationOptions _options;

        public SqlQueryService(IOptions<TestAutomationOptions> options)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Get DCR ClientId for a SoftwareProductID.
        /// </summary>
        /// <returns>string.</returns>
        public string GetClientId(string softwareProductId)
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(GetClientId), nameof(SqlQueryService));

            using var connection = new SqlConnection(_options.AUTHSERVER_CONNECTIONSTRING);

            var clientId = connection.QuerySingle<string>(
                "select clientid from clientclaims where Upper(type)=@ClaimType and upper(value)=@ClaimValue",
                new
                {
                    ClaimType = "SOFTWARE_ID",
                    ClaimValue = softwareProductId.ToUpper(),
                });

            if (string.IsNullOrEmpty(clientId))
            {
                throw new InvalidOperationException($"{nameof(GetClientId)} - ClientId not found for SoftwareProductId {softwareProductId}").Log();
            }

            return clientId;
        }

        public string GetStatus(EntityType entityType, string id)
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(GetStatus), nameof(SqlQueryService));

            using var connection = new SqlConnection(_options.AUTHSERVER_CONNECTIONSTRING);
            connection.Open();

            var statusColumnName = entityType == EntityType.SOFTWAREPRODUCT ? Status.STATUS.ToString() : $"{entityType}Status";
            using var selectCommand = new SqlCommand($"select {statusColumnName} from softwareproducts where {entityType}ID = @id", connection);
            selectCommand.Parameters.AddWithValue("@id", id);

            return selectCommand.ExecuteScalarString();
        }

        public void SetStatus(EntityType entityType, string id, string status)
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(SetStatus), nameof(SqlQueryService));

            using var connection = new SqlConnection(_options.AUTHSERVER_CONNECTIONSTRING);
            connection.Open();

            var statusColumnName = entityType == EntityType.SOFTWAREPRODUCT ? Status.STATUS.ToString() : $"{entityType}Status";
            using var updateCommand = new SqlCommand($"update softwareproducts set {statusColumnName} = @status where {entityType}ID = @id", connection);
            updateCommand.Parameters.AddWithValue("@id", id);
            updateCommand.Parameters.AddWithValue("@status", status);
            updateCommand.ExecuteNonQuery();

            if (string.Compare(GetStatus(entityType, id), status.ToString()) != 0)
            {
                throw new InvalidOperationException("Status update failed").Log();
            }
        }
    }
}
