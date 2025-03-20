﻿namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation
{
    public static class Constants
    {
        public const string Null = "***NULL***";
        public const string Omit = "***OMIT**";
        public const string GuidFoo = "f00f00f0-f00f-f00f-f00f-f00f00f00f00";

        public const string AuthoriseOTP = "000789";

        public const string ClientAssertionType = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer";

        public static class IdTypes
        {
            public const string IdTypeAccount = "ACCOUNT_ID";
            public const string IdTypeTransaction = "TRANSACTION_ID";
        }

        public static class AuthServer
        {
            public const int Days90 = 7776000; // 90 days
            public const int SharingDuration = Days90;

            public const int DefaultTokenLifetime = 3600;

            public const string FapiPhase2CodeVerifier = "foo-bar";
            public const string FapiPhase2CodeChallengeMethod = "S256";

            public const string ScopeTokenAccounts = "openid bank:accounts.basic:read";
        }

        /// <summary>
        /// User/Customer IDs used for testing. Industry specific users are in an industry sub-class whereas the rest are at the first level.
        /// </summary>
        public static class Users
        {
            /// <summary>
            /// Banking-specific Users.
            /// </summary>
            public static class Banking
            {
                public const string UserIdJaneWilson = "jwilson"; // Banking customer
                public const string CustomerIdJaneWilson = "bfb689fb-7745-45b9-bbaa-b21e00072447"; // Banking customer
            }

            /// <summary>
            /// Energy-specific Users.
            /// </summary>
            public static class Energy
            {
                public const string UserIdMaryMoss = "mmoss"; // Energy customer
                public const string UserIdHeddaHare = "hhare"; // Energy customer
                public const string CustomerIdMaryMoss = "db1ddad1-a033-4088-8d0f-c800ed462717"; // Energy customer
            }

            public const string UserIdSteveKennedy = "sken";
            public const string UserIdDewayneSteve = "dsteve";
            public const string UserIdBusiness1 = "bis1";
            public const string UserIdBusiness2 = "bis2";
            public const string UserIdBeverage = "bev";
            public const string UserIdKamillaSmith = "ksmith";

            public const string CustomerIdBusiness1 = "a97ba8d9-c89d-4126-a3b1-5aaa50f8dc5f";
        }

        public static class Accounts
        {
            /// <summary>
            /// Banking-specific Accounts.
            /// </summary>
            public static class Banking
            {
                public const string AccountIdJaneWilson = "98765988";
                public const string AccountIdsAllJaneWilson = "98765988,98765987"; // Banking customer
            }

            /// <summary>
            /// Energy-specific Accounts.
            /// </summary>
            public static class Energy
            {
                public const string AccountIdMaryMoss = "0011223301"; // Energy customer
                public const string AccountIdsAllMaryMoss = "0011223301,0011223302,0011223303,0011223304,0011223305,0011223306,0011223307,0011223308,0011223309,0011223310,0011223311,0011223312,0011223313,0011223314,0011223315,0011223316,0011223317,0011223318,0011223319,0011223320,0011223321"; // Energy customer
                public const string AccountIdsSubsetMaryMoss = "0011223301,0011223302,0011223303"; // Energy customer
                public const string AccountIdsAllHeddaHare = "4ee1a8db-13af-44d7-b54b-e94dff3df548"; // Energy customer
            }

            public const string AccountIdJohnSmith = "1122334455";
            public const string AccountIdKamillaSmith = "0000001";

            public const string AccountIdsAllBusiness1 = "54676423";
            public const string AccountIdsAllBusiness2 = "";
            public const string AccountIdsAllBeverage = "835672345";
            public const string AccountIdsAllKamillaSmith = "0000001,0000002,0000003,0000004,0000005,0000006,0000007,0000008,0000009,0000010,1000001,1000002,1000003,1000004,1000005,1000006,1000007,1000008,1000009,1000010,2000001,2000002,2000003,2000004,2000005,2000006,2000007,2000008,2000009,2000010";
            public const string AccountIdsAllDewayneSmith = "96565987,1100002,96534987";
            public const string AccountIdsAllSteveKennedy = "";
        }

        public static class AccessTokens
        {
            // VSCode slows on excessively long lines, splitting string constant into smaller lines.
            public const string DataHolderAccessTokenExpired =
                @"eyJhbGciOiJQUzI1NiIsImtpZCI6IjdDNTcxNjU1M0U5QjEzMkVGMzI1QzQ5Q0EyMDc5NzM3MTk2QzAzREIiLCJ4NXQiOiJmRmNXVlQ2YkV5N3pKY1Njb2dlWE54bHNBOXMiLCJ0eXAiOi" +
                @"JhdCtqd3QifQ.eyJuYmYiOjE2NTI2Njk5NDMsImV4cCI6MTY1MjY3MDI0MywiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODEwMSIsImF1ZCI6ImNkcy1hdSIsImNsaWVudF9pZCI6ImM2M" +
                @"zI3Zjg3LTY4N2EtNDM2OS05OWE0LWVhYWNkM2JiODIxMCIsImNsaWVudF9zb2Z0d2FyZV9pZCI6ImM2MzI3Zjg3LTY4N2EtNDM2OS05OWE0LWVhYWNkM2JiODIxMCIsImNsaWVudF9zb2Z" +
                @"0d2FyZV9zdGF0ZW1lbnQiOiJleUpoYkdjaU9pSlFVekkxTmlJc0ltdHBaQ0k2SWpVME1rRTVRamt4TmpBd05EZzRNRGc0UTBRMFJEZ3hOamt4TmtFNVJqUTBPRGhFUkRJMk5URWlMQ0owZ" +
                @"VhBaU9pSktWMVFpZlEuZXcwS0lDQWliR1ZuWVd4ZlpXNTBhWFI1WDJsa0lqb2dJakU0WWpjMVlUYzJMVFU0TWpFdE5HTTVaUzFpTkRZMUxUUTNNRGt5T1RGalpqQm1OQ0lzRFFvZ0lDSnN" +
                @"aV2RoYkY5bGJuUnBkSGxmYm1GdFpTSTZJQ0pOYjJOcklGTnZablIzWVhKbElFTnZiWEJoYm5raUxBMEtJQ0FpYVhOeklqb2dJbU5rY2kxeVpXZHBjM1JsY2lJc0RRb2dJQ0pwWVhRaU9pQ" +
                @"XhOalV5TmpZNU9USTRMQTBLSUNBaVpYaHdJam9nTVRZMU1qWTNNRFV5T0N3TkNpQWdJbXAwYVNJNklDSmpOREkzTXpjd1pEa3pOR0UwWTJObFlXUTBObU0xWWpGbE9EQmlaalZoTWlJc0R" +
                @"Rb2dJQ0p2Y21kZmFXUWlPaUFpWm1aaU1XTTRZbUV0TWpjNVpTMDBOR1E0TFRrMlpqQXRNV0pqTXpSaE5tSTBNelptSWl3TkNpQWdJbTl5WjE5dVlXMWxJam9nSWsxdlkyc2dSbWx1WVc1a" +
                @"lpTQlViMjlzY3lJc0RRb2dJQ0pqYkdsbGJuUmZibUZ0WlNJNklDSk5lVUoxWkdkbGRFaGxiSEJsY2lJc0RRb2dJQ0pqYkdsbGJuUmZaR1Z6WTNKcGNIUnBiMjRpT2lBaVFTQndjbTlrZFd" +
                @"OMElIUnZJR2hsYkhBZ2VXOTFJRzFoYm1GblpTQjViM1Z5SUdKMVpHZGxkQ0lzRFFvZ0lDSmpiR2xsYm5SZmRYSnBJam9nSW1oMGRIQnpPaTh2Ylc5amEzTnZablIzWVhKbEwyMTVZblZrW" +
                @"jJWMFlYQndJaXdOQ2lBZ0luSmxaR2x5WldOMFgzVnlhWE1pT2lCYkRRb2dJQ0FnSW1oMGRIQnpPaTh2Ykc5allXeG9iM04wT2prNU9Ua3ZZMjl1YzJWdWRDOWpZV3hzWW1GamF5SU5DaUF" +
                @"nWFN3TkNpQWdJbXh2WjI5ZmRYSnBJam9nSW1oMGRIQnpPaTh2Ylc5amEzTnZablIzWVhKbEwyMTVZblZrWjJWMFlYQndMMmx0Wnk5c2IyZHZMbkJ1WnlJc0RRb2dJQ0owYjNOZmRYSnBJa" +
                @"m9nSW1oMGRIQnpPaTh2Ylc5amEzTnZablIzWVhKbEwyMTVZblZrWjJWMFlYQndMM1JsY20xeklpd05DaUFnSW5CdmJHbGplVjkxY21raU9pQWlhSFIwY0hNNkx5OXRiMk5yYzI5bWRIZGh" +
                @"jbVV2YlhsaWRXUm5aWFJoY0hBdmNHOXNhV041SWl3TkNpQWdJbXAzYTNOZmRYSnBJam9nSW1oMGRIQnpPaTh2Ykc5allXeG9iM04wT2prNU9UZ3ZhbmRyY3lJc0RRb2dJQ0p5WlhadlkyR" +
                @"jBhVzl1WDNWeWFTSTZJQ0pvZEhSd2N6b3ZMMnh2WTJGc2FHOXpkRG81TURBeEwzSmxkbTlqWVhScGIyNGlMQTBLSUNBaWNtVmphWEJwWlc1MFgySmhjMlZmZFhKcElqb2dJbWgwZEhCek9" +
                @"pOHZiRzlqWVd4b2IzTjBPamt3TURFaUxBMEtJQ0FpYzI5bWRIZGhjbVZmYVdRaU9pQWlZell6TWpkbU9EY3ROamczWVMwME16WTVMVGs1WVRRdFpXRmhZMlF6WW1JNE1qRXdJaXdOQ2lBZ" +
                @"0luTnZablIzWVhKbFgzSnZiR1Z6SWpvZ0ltUmhkR0V0Y21WamFYQnBaVzUwTFhOdlpuUjNZWEpsTFhCeWIyUjFZM1FpTEEwS0lDQWljMk52Y0dVaU9pQWliM0JsYm1sa0lIQnliMlpwYkd" +
                @"VZ1ltRnVhenBoWTJOdmRXNTBjeTVpWVhOcFl6cHlaV0ZrSUdKaGJtczZZV05qYjNWdWRITXVaR1YwWVdsc09uSmxZV1FnWW1GdWF6cDBjbUZ1YzJGamRHbHZibk02Y21WaFpDQmlZVzVyT" +
                @"25CaGVXVmxjenB5WldGa0lHSmhibXM2Y21WbmRXeGhjbDl3WVhsdFpXNTBjenB5WldGa0lHTnZiVzF2YmpwamRYTjBiMjFsY2k1aVlYTnBZenB5WldGa0lHTnZiVzF2YmpwamRYTjBiMjF" +
                @"sY2k1a1pYUmhhV3c2Y21WaFpDQmpaSEk2Y21WbmFYTjBjbUYwYVc5dUlHVnVaWEpuZVRwaFkyTnZkVzUwY3k1aVlYTnBZenB5WldGa0lHVnVaWEpuZVRwaFkyTnZkVzUwY3k1amIyNWpaW" +
                @"E56YVc5dWN6cHlaV0ZrSWcwS2ZRLnUzU0RhSW1fR21SbDg2Yi1hU3B3OHBYNlhCaVp2VVJRWUFJU2QyVXY0MUNVZlZmYXRSN3RZWWlEaVZhVWt5c1FUSW5sazNnVE85Yk5pT0lqM041Z0l" +
                @"kYmRQUXFSTmRVNTB5emxMN1RlVGhmc2JadTZsc0hVdVR6RWVUVlNBZzVMNGlPZk9OZndEYXRMSjBrTlR3b0ZPVWRZSmt4dnFLd0RJazl0Ymw4ZVpkbG1Rc21SQmNmN3oyMnBGUlQzaDJuT" +
                @"FNMWndIaEU2ck9OTEhFbk5YSjhLbUpzaXBnWmtzcFJPRzZXanZMUVl6MEtNVVdxN01EV3Y1TklIdFhXTFBsWlFpNjBLSHJYYkhxcXJkN3ZmRTRMWDFrMFRkUi1UeDFZeHpoT2EteGdvUWx" +
                @"qS1M2ck9qal9uWXl0el9WSDBTUWd5czVCX3ZOSjRtQk9qNi1JQlgzRmdSZyIsImNsaWVudF9sb2dvX3VyaSI6Imh0dHBzOi8vbW9ja3NvZnR3YXJlL215YnVkZ2V0YXBwL2ltZy9sb2dvL" +
                @"nBuZyIsImNsaWVudF9qd2tzX3VyaSI6Imh0dHBzOi8vbG9jYWxob3N0Ojk5OTgvandrcyIsImNsaWVudF90b2tlbl9lbmRwb2ludF9hdXRoX21ldGhvZCI6InByaXZhdGVfa2V5X2p3dCI" +
                @"sImNsaWVudF90b2tlbl9lbmRwb2ludF9hdXRoX3NpZ25pbmdfYWxnIjoiUFMyNTYiLCJjbGllbnRfaWRfdG9rZW5fZW5jcnlwdGVkX3Jlc3BvbnNlX2FsZyI6IlJTQS1PQUVQIiwiY2xpZ" +
                @"W50X2lkX3Rva2VuX2VuY3J5cHRlZF9yZXNwb25zZV9lbmMiOiJBMjU2R0NNIiwiY2xpZW50X2lkX3Rva2VuX3NpZ25lZF9yZXNwb25zZV9hbGciOiJQUzI1NiIsImNsaWVudF9vcmdfaWQ" +
                @"iOiJmZmIxYzhiYS0yNzllLTQ0ZDgtOTZmMC0xYmMzNGE2YjQzNmYiLCJjbGllbnRfb3JnX25hbWUiOiJNb2NrIEZpbmFuY2UgVG9vbHMiLCJjbGllbnRfcmV2b2NhdGlvbl91cmkiOiJod" +
                @"HRwczovL2xvY2FsaG9zdDo5MDAxL3Jldm9jYXRpb24iLCJjbGllbnRfY2xpZW50X2lkX2lzc3VlZF9hdCI6IjE2NTI2Njk5MzgiLCJjbGllbnRfYXBwbGljYXRpb25fdHlwZSI6IndlYiI" +
                @"sImNsaWVudF9wb2xpY3lfdXJpIjoiaHR0cHM6Ly9tb2Nrc29mdHdhcmUvbXlidWRnZXRhcHAvcG9saWN5IiwiY2xpZW50X3Rvc191cmkiOiJodHRwczovL21vY2tzb2Z0d2FyZS9teWJ1Z" +
                @"GdldGFwcC90ZXJtcyIsImNsaWVudF9sZWdhbF9lbnRpdHlfaWQiOiIxOGI3NWE3Ni01ODIxLTRjOWUtYjQ2NS00NzA5MjkxY2YwZjQiLCJjbGllbnRfbGVnYWxfZW50aXR5X25hbWUiOiJ" +
                @"Nb2NrIFNvZnR3YXJlIENvbXBhbnkiLCJjbGllbnRfcmVjaXBpZW50X2Jhc2VfdXJpIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6OTAwMSIsImNsaWVudF9zZWN0b3JfaWRlbnRpZmllcl91cmkiO" +
                @"iJsb2NhbGhvc3QiLCJqdGkiOiI5V0U4cWJ0Ukhpa29neURTc1gySXJnIiwic29mdHdhcmVfaWQiOiJjNjMyN2Y4Ny02ODdhLTQzNjktOTlhNC1lYWFjZDNiYjgyMTAiLCJzZWN0b3JfaWR" +
                @"lbnRpZmllcl91cmkiOiJsb2NhbGhvc3QiLCJjbmYiOnsieDV0I1MyNTYiOiI3MTVDREQwNEZGNzMzMkNDREE3NENERjlGQkVEMTZCRUJBNURENzQ0In0sInNjb3BlIjpbImNkcjpyZWdpc" +
                @"3RyYXRpb24iXX0.gg7pSRRLYpM69cDBg83LijyKOvnHf79ySsUe007s4Yy0eFe0ALnA_sbdxyaeoARVc0Rftg0Mpck6PLE0u3zJAsuNm6tV4r3jVf5m38EbvYB5N8cl18Z04PjoKvhVKhZ" +
                @"5yM3wHYM6_eelSvv0aWkptRYDDcVCa4_H93RmiPJt5RoUX2SR7lf8gdHM9fb-n1_OcIVdEDz9W6RUw1o3TFp5kh3xIlS_sIawJ5dGTztnj3VtI36d7qL59uPojPmUQ-OT22IZE-u_KZxAe" +
                @"tUQhX0-IqUzdVsdTf2t9DNva2VRkK9Cdf2kCtqs17NGDlWceQ7IKR-U6qn9izNOYeM47Qhqa6MiROtrfe5Ja3p8vjnN72eEQ_XPd2bMVxkbyh0IrG9-5JCOolbjjnbZaxh4dIggfdY52JS" +
                @"2-DLYhQnMnJtrVkKe1J212x8SVf7FKcNGY0OM4MLG3Gcl5S8EzQQuh464Nr-rPnec7SbrqjQ2xyn56s4Nhv5PfQ-VOqPQXOkyBPzH";

            // VSCode slows on excessively long lines, splitting string constant into smaller lines.
            public const string ConsumerAccessTokenBankingExpired =
                @"eyJhbGciOiJQUzI1NiIsImtpZCI6IjdDNTcxNjU1M0U5QjEzMkVGMzI1QzQ5Q0EyMDc5NzM3MTk2QzAzREIiLCJ4NXQiOiJmRmNXVlQ2YkV5N3pKY1Njb2dlWE54bHNBOXMiLCJ0eXAiOiJhdCtqd3QifQ.eyJuYmYiOjE2NTI2ODM2NDksImV4cCI6MTY1MjY4NzI0OSwiaXNzIjoiaHR0cHM6Ly9tb2NrLWRh" +
                @"dGEtaG9sZGVyOjgwMDEiLCJhdWQiOiJjZHMtYXUiLCJjbGllbnRfaWQiOiJjNjMyN2Y4Ny02ODdhLTQzNjktOTlhNC1lYWFjZDNiYjgyMTAiLCJhdXRoX3RpbWUiOjE2NTI2ODM2NDksImlkcCI6ImxvY2FsIiwic2hhcmluZ19leHBpcmVzX2F0IjoxNjYwNDU5NjQ5LCJjZHJfYXJyYW5nZW1lbnRfaWQiOiI" +
                @"xZjc4YTY3ZS0xZDhkLTRiNzctODI3OC04NTNiMmQ5NTM5YjMiLCJqdGkiOiItNWZ0ZTJ1azMwTGZsa1g2N2JFVUx3Iiwic29mdHdhcmVfaWQiOiJjNjMyN2Y4Ny02ODdhLTQzNjktOTlhNC1lYWFjZDNiYjgyMTAiLCJzZWN0b3JfaWRlbnRpZmllcl91cmkiOiJtb2NrLWRhdGEtaG9sZGVyLWludGVncmF0aW" +
                @"9uLXRlc3RzIiwiYWNjb3VudF9pZCI6WyJiMjNtTnRTWGdwU0s2WVFsMGdOcE9NVUxlQUhFbnpRMW54YXJiTzlTUDl1NWpEbjNhOWw0RDV6azd3YndFcVFiIiwiVG1vNW96aStNeHU5cVNVQVJCeWpKMjh2VjJrM2srRVBGMHBicHJsODVFSDhJJTJGTnVaTERRb09TbW9NZTRGeVpkIl0sInN1YiI6IkgvK0VrU" +
                @"0xWeE1nZjhwNXlJS3BScFNzTlRSZVk5VTZlYTNGWVZtUWFSL1ZwQ2pHVGpSenA4YTdDOXVjWEk3ak0iLCJjbmYiOnsieDV0I1MyNTYiOiI3MTVDREQwNEZGNzMzMkNDREE3NENERjlGQkVEMTZCRUJBNURENzQ0In0sInNjb3BlIjpbIm9wZW5pZCIsInByb2ZpbGUiLCJiYW5rOmFjY291bnRzLmJhc2ljOnJl" +
                @"YWQiLCJiYW5rOnRyYW5zYWN0aW9uczpyZWFkIiwiY29tbW9uOmN1c3RvbWVyLmJhc2ljOnJlYWQiXSwiYW1yIjpbInB3ZCJdfQ.g_x-MZa6Lq_2m3D0DKKk9SEhHs2TUIF_3oyWf2E18873KI3q6YCom7zFYpIrxrtgmcW8jN1gSMZFx_b9FhvWXRCL48GFLgVmqUml6yPNLH9Oa95UziIS58ROSxkOkidaIEbM" +
                @"CIfE-6jTl_VzYxE7G19STYYbC_zU3e8hkgDShdh-KpKHW_TgWd_gvHAwHYNJF8TeFnXiAZSOSd4bfO2v9hjDWRN1SA0O-dkZssfthNZxGCsBc0yJfGYi5887KsuWhH1EMTWcAXRAfImeRa6rSgvTZu9imFFzomzdHR5KVD_L5Dq0Q4JtAlu4TmT5RIMWmQEaz7G3JvTfMAxfXkBEqe2UNP4Bm7Npgat9eCH6SS1" +
                @"daaIJExpJF9C61C1PuV7t1fDHH3pz30H2rBJWZ_gXZJc2xUvw9oAFiJC10iB4dfd1nLgRbFfMx-i84aOfPm6bH-IoTEk1iJ4Odm-FkI9S_MO1rlpYVcuEBCuUKri6PCYKzHU4_istqN8x7bQ_kQQf";

            public const string ConsumerAccessTokenEnergyExpired =
                @"eyJhbGciOiJQUzI1NiIsImtpZCI6IjdDNTcxNjU1M0U5QjEzMkVGMzI1QzQ5Q0EyMDc5NzM3MTk2QzAzREIiLCJ4NXQiOiJmRmNXVlQ2YkV5N3pKY1Njb2dlWE54bHNBOXMiLCJ0eXAiOiJhdCtqd3QifQ.eyJuYmYiOjE2NTI0Mjc2MTYsImV4cCI6MTY1MjQzMTIxNiwiaXNzIjoiaHR0cHM6Ly9tb2NrLWRh" +
                @"dGEtaG9sZGVyLWVuZXJneTo4MTAxIiwiYXVkIjoiY2RzLWF1IiwiY2xpZW50X2lkIjoiYzYzMjdmODctNjg3YS00MzY5LTk5YTQtZWFhY2QzYmI4MjEwIiwiYXV0aF90aW1lIjoxNjUyNDI3NjE2LCJpZHAiOiJsb2NhbCIsImNkcl9hcnJhbmdlbWVudF9pZCI6ImNiMjRhNWYxLTFjMzYtNGFmMS04MzYyLTc4" +
                @"ZmRlNWJhMjIyMCIsImp0aSI6IlJjY18teHhnUEtpN1hHWlh2S0lvVlEiLCJzb2Z0d2FyZV9pZCI6ImM2MzI3Zjg3LTY4N2EtNDM2OS05OWE0LWVhYWNkM2JiODIxMCIsInNlY3Rvcl9pZGVudGlmaWVyX3VyaSI6Im1vY2stZGF0YS1ob2xkZXItZW5lcmd5LWludGVncmF0aW9uLXRlc3RzIiwiYWNjb3VudF9p" +
                @"ZCI6WyJqdWhJNWZiM1dGVVAlMkZkQmZpeDJYRkRWMWs4amxkRHpwTlphVWRoVzI2UG82TXZFWHFGZlhrNXNudUI3RGkwSHEiLCJqdWhJNWZiM1dGVVAlMkZkQmZpeDJYRkRWMWs4amxkRHpwTlphVWRoVzI2UHBseTZSRVZTc2N3WDJOJTJGK1BjeDk2ZCIsImlpcGx5bzJENGhyM25mN1AzY1hkdVJzNlE3USUy" +
                @"RlhXUVJ0QXpkcG1MM3AwUG14MkxYdXM0bnl4WUdRN3JBeHNRNCIsImp1aEk1ZmIzV0ZVUCUyRmRCZml4MlhGRFYxazhqbGREenBOWmFVZGhXMjZQb0wlMkZURnd2bERWelFLdG1yV0FsSm83IiwiZCUyRnJzQ1dRbmV5TFNOUm1URm0lMkYrZGM0RUk4aEUxOXUzS0s2M2VYVVNEZDBXVlFKTnRqcEpYZWZtdUVpc" +
                @"DRiOWMiLCJqdWhJNWZiM1dGVVAlMkZkQmZpeDJYRkRWMWs4amxkRHpwTlphVWRoVzI2UG9DcGdMelV2cCtUSitrTzdjQkJRK20iLCJqdWhJNWZiM1dGVVAlMkZkQmZpeDJYRkRWMWs4amxkRHpwTlphVWRoVzI2UG9DQkVWVTJHZkc4NWpNU2doR3FPWXUiLCJhMEdYTzhzZStNZHZTRDBtbXZUVnJCTkZ4ZXJzRH" +
                @"NKV3dHTlFRS3pvUVBoMU03N2dXQVNNU0c3Z3RZUUZZbDdkIiwiMG9uT3FQYVQrZGtYR0RrZzc1a1ZFREhtT3l3S1dVbW9sbmdhMFNaT0RTTEhoRXYlMkZWV3BjNVVHNjhiaExUdExjIiwianVoSTVmYjNXRlVQJTJGZEJmaXgyWEZEVjFrOGpsZER6cE5aYVVkaFcyNlBvV3RLdjROR2FWRlVJNmFHbFN2UVBVIiwi" +
                @"anVoSTVmYjNXRlVQJTJGZEJmaXgyWEZEVjFrOGpsZER6cE5aYVVkaFcyNlBvR0xBNWt5cU1xVTNzZEkwZm1ZVWlxIiwianVoSTVmYjNXRlVQJTJGZEJmaXgyWEZEVjFrOGpsZER6cE5aYVVkaFcyNlBwTU9BbkpGY2tkRm5hU2MxNUVjUUJNIiwiaWlwbHlvMkQ0aHIzbmY3UDNjWGR1UnM2UTdRJTJGWFdRUnRBemRw" +
                @"bUwzcDBQMTBPZjVlR0ZxNlFWM3paMFlBcmt4IiwianVoSTVmYjNXRlVQJTJGZEJmaXgyWEZEVjFrOGpsZER6cE5aYVVkaFcyNlBwaElpTzhRczAyV0RzWVhPRWg1SyUyRk0iLCJkJTJGcnNDV1FuZXlMU05SbVRGbSUyRitkYzRFSThoRTE5dTNLSzYzZVhVU0RkMHpFV3JjNDVqeHN1RmU3aGxJQjFqViIsImp1aEk1Z" +
                @"mIzV0ZVUCUyRmRCZml4MlhGRFYxazhqbGREenBOWmFVZGhXMjZQb1FiOUthVlFXbnJqNVNkbHJucmc2OSIsImp1aEk1ZmIzV0ZVUCUyRmRCZml4MlhGRFYxazhqbGREenBOWmFVZGhXMjZQcHBtUG5BWk5pVEtDa3dyRWtKQ0x6YSIsImEwR1hPOHNlK01kdlNEMG1tdlRWckJORnhlcnNEc0pXd0dOUVFLem9RUGlXO" +
                @"E1OK2l1cm1RNVE2MmVpdm93TUgiLCIwb25PcVBhVCtka1hHRGtnNzVrVkVESG1PeXdLV1Vtb2xuZ2EwU1pPRFNKNm13QktlTUVkWkFQaWRsOHIzY2dlIiwianVoSTVmYjNXRlVQJTJGZEJmaXgyWEZEVjFrOGpsZER6cE5aYVVkaFcyNlBxNFFaK3FSMXRwdEpERjYlMkZiSTJkWkgiLCJqdWhJNWZiM1dGVVAlMkZkQm" +
                @"ZpeDJYRkRWMWs4amxkRHpwTlphVWRoVzI2UHJkWjlxY080MG1HdHpySHFCZElOT3giXSwic3ViIjoicEF2YklpNC8vcGY5ZXIwMkxReG14eWx0UTRlcGduY0FPSFM2UmlmYXo5MVVXakVJOS9QQlJwdlk5SlNGaGhMZSIsImNuZiI6eyJ4NXQjUzI1NiI6IjcxNUNERDA0RkY3MzMyQ0NEQTc0Q0RGOUZCRUQxNkJFQkE" +
                @"1REQ3NDQifSwic2NvcGUiOlsib3BlbmlkIiwicHJvZmlsZSIsImVuZXJneTphY2NvdW50cy5iYXNpYzpyZWFkIiwiZW5lcmd5OmFjY291bnRzLmNvbmNlc3Npb25zOnJlYWQiLCJjb21tb246Y3VzdG9tZXIuYmFzaWM6cmVhZCJdLCJhbXIiOlsicHdkIl19.Mk9S5KSRfY04to3GrfP941afq__ac1owJv-6G-w9s6H" +
                @"p02xUhOdZ8uTIRJMPKLJ_rcaL9gD3ONqNg_OpScbdnObaSwFanbnL9qiMo2MpyyWwjvlQaSUB8rr1iqOoooJ4HIg5P47XVdbNgy3_TlELd0f97pFUksLdjKfNO9WYZePd1HUg4oOmDpch4WPyvsG2bB3tVL29P8ffRWbj76Cg9PMJaPf97NKLXKrdwMW6WWOFa4vbPEOXTIFp9d6bGjhDXV3mgl3Kt49PQOJx8xAlh0ssU" +
                @"YzdIPNmwplq6J2SwOGcwz8dU-iSYc-cMlLnu3lftFNqnONVahMKXlKZoZPeUEdrWFhBoEro7_-EbiZhRfzAvFLTM__GtaVMnAUfsQ5GMtwVvTY17LWPUihnOCds6TJg3avMS31wi0OkMDb7HHkYHAjXvWrlvv7-8TyTbsEesNlyaam-DYDMkXE7vHD_5sMPA_8R0WRxiNV3X0rMHM9kBogbkwwG4mtsdd8fafWv";
        }

        public static class Certificates
        {
            // Certificate used by DataRecipient for the MTLS connection with Dataholder
            public const string DataHolderMtlsCertificateFilename = "Certificates/server.pfx";
            public const string DataholderMtlsCertificatePassword = "#M0ckDataHolder#";

            // Certificate used by DataRecipient to sign client assertions
            public const string CertificateFilename = "Certificates/client.pfx";
            public const string CertificatePassword = "#M0ckDataRecipient#";

            public const string JwtCertificateFilename = "Certificates/MDR/jwks.pfx";
            public const string JwtCertificatePassword = "#M0ckDataRecipient#";

            // An additional certificate used by DataRecipient to sign client assertions
            public const string AdditionalCertificateFilename = "Certificates/client-additional.pfx";
            public const string AdditionalCertificatePassword = CertificatePassword;

            public const string InvalidCertificateFilename = "Certificates/client-invalid.pfx";
            public const string InvalidCertificatePassword = "#M0ckDataRecipient#";

            public const string DataHolderCertificateFilename = "Certificates/mock-data-holder.pfx";
            public const string DataHolderCertificatePassword = "#M0ckDataHolder#";

            public const string AdditionalJwksCertificateFilename = AdditionalCertificateFilename;
            public const string AdditionalJwksCertificatePassword = AdditionalCertificatePassword;
        }

        public static class LegalEntities
        {
            public const string LegalEntityId = "18B75A76-5821-4C9E-B465-4709291CF0F4";
        }

        public static class Brands
        {
            public const string BrandId = "FFB1C8BA-279E-44D8-96F0-1BC34A6B436F";
            public const string AdditionalBrandId = "20C0864B-CEEF-4DE0-8944-EB0962F825EB";
        }

        public static class SoftwareProducts
        {
            public const string SoftwareProductId = "c6327f87-687a-4369-99a4-eaacd3bb8210";
            public const string InvalidSoftwareProductId = "f00f00f0-f00f-f00f-f00f-f00f00f00f00";
            public const string AdditionalSoftwareProductId = "9381DAD2-6B68-4879-B496-C1319D7DFBC9";

            public const string SoftwareProductSectorIdentifierUri = "api.mocksoftware";
        }

        public static class Scopes
        {
            // Scope
            public const string ScopeBanking = "openid profile common:customer.basic:read bank:accounts.basic:read bank:transactions:read"; // Also used by Auth Server
            public const string ScopeBankingWithoutOpenId = "profile common:customer.basic:read bank:accounts.basic:read bank:transactions:read";  // Also used by Auth Server
            public const string ScopeEnergy = "openid profile common:customer.basic:read energy:accounts.basic:read energy:accounts.concessions:read";  // scope of 'energy:accounts.concessions:read' is not working?
            public const string ScopeEnergyWithoutOpenId = "profile common:customer.basic:read energy:accounts.basic:read energy:accounts.concessions:read"; // energy:accounts.concessions:read is not working?
            public const string ScopeRegistration = "cdr:registration";
        }

        public static class IdPermanence
        {
            public const string IdPermanencePrivateKey = "90733A75F19347118B3BE0030AB590A8";
        }

        public static class ErrorMessages
        {
            public static class Par
            {
                public const string ParRequestUriFormParameterNotSupported = "ERR-PAR-001: request_uri form parameter is not supported";
                public const string ParRequestIsNotWellFormedJwt = "ERR-PAR-002: request is not a well-formed JWT";
                public const string UnsupportedChallengeMethod = "ERR-PAR-003: Unsupported code_challenge_method";
                public const string CodeChallengeInvalidLength = "ERR-PAR-004: Invalid code_challenge - invalid length";
                public const string CodeChallengeMissing = "ERR-PAR-005: code_challenge is missing";
                public const string RequestObjectJwtRequestUriNotSupported = "ERR-PAR-006: request_uri is not supported in request object";
                public const string RequestObjectJwtRedirectUriMissing = "ERR-PAR-007: redirect_uri missing from request object JWT";
                public const string RequesObjectJwtClientIdMismatch = "ERR-PAR-008: client_id does not match client_id in request object JWT";
                public const string MissingResponseMode = "ERR-PAR-009: response_mode is missing or not set to 'jwt' for response_type of 'code'";
                public const string ResponseTypeNotRegistered = "ERR-PAR-010: response_type is not registered for the client. Check client registration metadata.";
            }

            public static class Mtls
            {
                public const string MtlsMultipleThumbprints = "ERR-MTLS-001: Multiple certificate thumbprints found on request";
                public const string MtlsNoCertificate = "ERR-MTLS-002: No client certificate was found on request";
            }

            public static class Authorization
            {
                public const string AuthorizationHolderOfKeyCheckFailed = "ERR-AUTH-001: Holder of Key check failed";
                public const string AuthorizationAccessTokenExpired = "ERR-AUTH-002: Access Token check failed - it has been revoked";
                public const string AuthorizationInsufficientScope = "ERR-AUTH-003: Insufficent scope"; // This doesn't match the one in AuthServer because it had no additional text
                public const string RequestUriClientIdMismatch = "ERR-AUTH-0004: client_id does not match request_uri client_id";
                public const string RequestUriAlreadyUsed = "ERR-AUTH-005: request_uri has already been used";
                public const string ExpiredRequestUri = "ERR-AUTH-006: request_uri has expired";
                public const string InvalidRequestUri = "ERR-AUTH-007: Invalid request_uri";
                public const string MissingRequestUri = "ERR-AUTH-008: request_uri is missing";
                public const string AccessDenied = "ERR-AUTH-009: User cancelled the authorisation flow";
            }

            public static class ClientAssertion
            {
                public const string ClientAssertionTypeNotProvided = "ERR-CLIENT_ASSERTION-002: client_assertion_type not provided";
                public const string InvalidClientAssertionType = "ERR-CLIENT_ASSERTION-003: client_assertion_type must be urn:ietf:params:oauth:client-assertion-type:jwt-bearer";
                public const string ClientAssertionClientIdMismatch = "ERR-CLIENT_ASSERTION-004: client_id does not match client_assertion";
                public const string ClientAssertionInvalidFormat = "ERR-CLIENT_ASSERTION-005: Cannot read client_assertion.  Invalid format.";
                public const string ClientAssertionNotReadable = "ERR-CLIENT_ASSERTION-006: Cannot read client_assertion";
                public const string ClientAssertionSubjectIssNotSetToClientId = "ERR-CLIENT_ASSERTION-007: Invalid client_assertion - 'sub' and 'iss' must be set to the client_id";
                public const string ClientAssertionSubjectIssNotSameValue = "ERR-CLIENT_ASSERTION-008: Invalid client_assertion - 'sub' and 'iss' must have the same value";
                public const string ClientAssertionMissingIssClaim = "ERR-CLIENT_ASSERTION-009: Invalid client_assertion - Missing 'iss' claim";
            }

            public static class CdrArrangement
            {
                public const string InvalidConsentCdrArrangement = "ERR-ARR-001: Invalid Consent Arrangement";
            }

            public static class Jwt
            {
                public const string JwtInvalidAudience = "ERR-JWT-001: {0} - Invalid audience";
                public const string JwtExpired = "ERR-JWT-002: {0} has expired";
                public const string JwksError = "ERR-JWT-003: {0} - jwks error";
                public const string JwtValidationErro = "ERR-JWT-004: {0} - token validation error";
            }

            public static class Dcr
            {
                public const string DuplicateRegistration = "ERR-DCR-001: Duplicate registrations for a given software_id are not valid.";
                public const string EmptyRegistrationRequest = "ERR-DCR-002: Registration request is empty";
                public const string RegistrationRequestInvalidRedirectUri = "ERR-DCR-003: The redirect_uri '{0}' is not valid as it is not included in the software_statement";
                public const string RegistrationRequestValidationFailed = "ERR-DCR-004: Client Registration Request validation failed.";
                public const string SsaValidationFailed = "ERR-DCR-005: SSA validation failed.";
                public const string SoftwareStatementEmptyOrInvalid = "ERR-DCR-006: The software_statement is empty or invalid";
                public const string InvalidSectorIdentifierUri = "ERR-DCR-007: Invalid sector_identifier_uri";
            }

            public static class Token
            {
                public const string ExpiredRefreshToken = "ERR-TKN-001: refresh_token has expired";
                public const string InvalidRefreshToken = "ERR-TKN-002: refresh_token is invalid";
                public const string MissingRefreshToken = "ERR-TKN-003: refresh_token is missing";
                public const string InvalidCodeVerifier = "ERR-TKN-004: Invalid code_verifier";
                public const string AuthorizationCodeExpired = "ERR-TKN-005: authorization code has expired";
                public const string MissingCodeVerifier = "ERR-TKN-006: code_verifier is missing";
                public const string InvalidAuthorizationCode = "ERR-TKN-007: authorization code is invalid";
            }

            public static class General
            {
                public const string SoftwareProductNotFound = "ERR-GEN-001: Software product not found";
                public const string SoftwareProductStatusInactive = "ERR-GEN-002: Software product status is {0}";
                public const string SoftwareProductRemoved = "ERR-GEN-003: Software product status is removed - consents cannot be revoked";
                public const string ClientNotFound = "ERR-GEN-004: Client not found";
                public const string MissingClientId = "ERR-GEN-005: client_id is missing";
                public const string InvalidClientId = "ERR-GEN-006: Invalid client_id";
                public const string InvalidRedirectUri = "ERR-GEN-007: Invalid redirect_uri for client";
                public const string MissingResponseType = "ERR-GEN-008: response_type is missing";
                public const string UnsupportedResponseType = "ERR-GEN-009: response_type is not supported";
                public const string ResponseTypeMismatchRequestUriResponseType = "ERR-GEN-010: response_type does not match request_uri response_type";
                public const string MissingScope = "ERR-GEN-011: scope is missing";
                public const string MissingOpenIdScope = "ERR-GEN-012: openid scope is missing";
                public const string UnsupportedResponseMode = "ERR-GEN-013: response_mode is not supported"; // This was INVALID_RESPONSE_MODE in auth server
                public const string GrantTypeNotProvided = "ERR-GEN-014: grant_type not provided";
                public const string UnsupportedGrantType = "ERR-GEN-015: unsupported grant_type";
                public const string MissingIssuerClaim = "ERR-GEN-016: Missing iss claim";
                public const string JtiRequired = "ERR-GEN-017: Invalid client_assertion - 'jti' is required";
                public const string JtiNotUnique = "ERR-GEN-018: Invalid client_assertion - 'jti' must be unique";
                public const string ClientAssertionNotProvided = "ERR-GEN-019: client_assertion not provided";
                public const string InvalidJwksUri = "ERR-GEN-020: Invalid jwks_uri in SSA";
                public const string UnableToLoadJwksDataRecipient = "ERR-GEN-021: Could not load JWKS from Data Recipient endpoint: {0}";
                public const string UnableToLoadJwksFromRegister = "ERR-GEN-022: Could not load SSA JWKS from Register endpoint: {0}";
                public const string MissingExp = "ERR-GEN-023: Invalid request - exp is missing";
                public const string MissingNbf = "ERR-GEN-024: Invalid request - nbf is missing";
                public const string ExpiryGreaterThan60AfterNbf = "ERR-GEN-025: Invalid request - exp claim cannot be longer than 60 minutes after the nbf claim";
                public const string InvalidResponseModeForResponseType = "ERR-GEN-026: Invalid response_mode for response_type";
                public const string ScopeTooLong = "ERR-GEN-027: Invalid scope - scope is too long";
                public const string InvalidClaims = "ERR-GEN-028: Invalid claims in request object";
                public const string InvalidCdrArrangementId = "ERR-GEN-029: Invalid cdr_arrangement_id";
                public const string InvalidNonce = "ERR-GEN-030: Invalid nonce";
                public const string InvalidTokenRequest = "ERR-GEN-031: invalid token request";
                public const string MissingGrantType = "ERR-GEN-032: grant_type is missing";
                public const string ClientIdMismatch = "ERR-GEN-033: client_id does not match";
                public const string UnableToRetrieveClientMetadata = "ERR-GEN-034: Could not retrieve client metadata";
                public const string MissingCode = "ERR-GEN-035: code is missing";
                public const string MissingRedirectUri = "ERR-GEN-036: redirect_uri is missing";
                public const string RedirectUriAuthorizationRequestMismatch = "ERR-GEN-037: redirect_uri does not match authorization request";
                public const string InvalidClient = "ERR-GEN-038: invalid_client";
            }
        }

        public static class LogTemplates
        {
            public const string StartedFunctionInClass = "Started {FunctionName} in {ClassName}.";
        }
    }
}
