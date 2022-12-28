using RARIndia.DataAccessLayer;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

using System;

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;
namespace RARIndia.BusinessLogicLayer
{
    public class UserMasterBA : BaseBusinessLogic
    {
        UserMasterDAL _userMasterDAL = null;
        public UserMasterBA()
        {
            _userMasterDAL = new UserMasterDAL();
        }

        public UserLoginViewModel Login(UserLoginViewModel userLoginViewModel)
        {
            try
            {
                userLoginViewModel.Password = MD5Hash(userLoginViewModel.Password);
                UserModel userModel = _userMasterDAL.Login(userLoginViewModel.ToModel<UserModel>());
                if (IsNotNull(userModel))
                {
                    SaveInSession<UserModel>(RARIndiaConstant.UserDataSession, userModel);
                }
                return userLoginViewModel;
            }
            catch (RARIndiaException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.NotFound:
                        return (UserLoginViewModel)GetViewModelWithErrorMessage(userLoginViewModel, GeneralResources.ErrorMessage_ThisaccountdoesnotexistEnteravalidemailaddressorpassword);
                    default:
                        return (UserLoginViewModel)GetViewModelWithErrorMessage(userLoginViewModel, GeneralResources.ErrorMessage_PleaseContactYourAdministrator);
                }
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.User.ToString());
                return (UserLoginViewModel)GetViewModelWithErrorMessage(userLoginViewModel, GeneralResources.ErrorMessage_PleaseContactYourAdministrator);
            }
        }
    }
}
