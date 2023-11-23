using System.ComponentModel.DataAnnotations;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models
{
    public class Error
    {
        public Error()
        {
        }

        public Error(string code, string title, string detail)
        {
            Code = code;
            Title = title;
            Detail = detail;
        }

        /// <summary>
        /// Error code.
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Error title.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Error detail.
        /// </summary>
        [Required]
        public string Detail { get; set; }

        /// <summary>
        /// Optional additional data for specific error types.
        /// </summary>
        public object Meta { get; set; }
    }
}
