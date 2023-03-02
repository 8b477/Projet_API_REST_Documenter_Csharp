//using Udemy.Projet.API.REST.Models;

//namespace Projet.API.REST.Swagger.ModelCheck
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class CleanUpModel
//    {
//        #region Méthode priver
//        private bool ValidateModel(TodoListmodel model)
//        {
//            if (model == null)
//            {
//                return false;
//            }

//            if (string.IsNullOrWhiteSpace(model.Title))
//            {
//                return false;
//            }

//            if (model.Title.Length < 5 || model.Title.Length > 100)
//            {
//                return false;
//            }

//            if (string.IsNullOrWhiteSpace(model.Email))
//            {
//                return false;
//            }

//            try
//            {
//                var mailAddress = new System.Net.Mail.MailAddress(model.Email);
//                return mailAddress.Address == model.Email;
//            }
//            catch (FormatException)
//            {
//                return false;
//            }

//            // Si toutes les vérifications ont réussi, alors le modèle est valide
//            return true;
//        }
//        #endregion
//    }
//}
