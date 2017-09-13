using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSIFYP.Models
{
    public abstract class Person
    {
        
        public int ID { get; set; }

        public string father { get; set; }

        [Required]
        [StringLength(7, ErrorMessage = "*")]
        public string gender { get; set; }


        [Required]
        [StringLength(5, ErrorMessage = "*")]
        public string blood { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "*")]
        public string datebirth { get; set; }



        [Required]
        [StringLength(15, ErrorMessage = "*")]
        public string district { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "*")]
        public string nationality { get; set; }


        public string userId{get;set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        [Email]
        //[StringLength(15, ErrorMessage = "*")]
        public string email { get; set; }

        public int getId { get { return ID; } }

        public string noper { get; set; }
        public string personcnic { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "*")]
        public string Adddress { get; set; }


        //[Required]
        //[StringLength(50, ErrorMessage = "*")]
        public string imgUrl { get; set; }


        [Required]
        [StringLength(20, ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string password { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassowrd { get; set; }

        public int logCont { get; set; }

        public string Role { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstMidName + " , " + LastName;
            }
        }
        public string getUsername { get {
                return FullName;
            } }
    }
}