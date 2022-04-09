using Lear.CRS.Common.CustomExceptions;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lear.CRS.Common
{
    public class ActiveDirectory
    {
        string DomainPath = "LDAP://DC=corp,DC=lear,DC=com";
        DirectoryEntry Entry;
        DirectorySearcher Search;
        SearchResult ResultSet;
        public ActiveDirectory()
        {
            Entry = new DirectoryEntry(DomainPath);
            Entry = new DirectoryEntry();
            Search = new DirectorySearcher(Entry);
        }
        public bool? GetADUserActiveStatusByDomainUserName(string domainUserName)
        {
            bool? Enabled = false;
            PrincipalContext context = new PrincipalContext(ContextType.Domain);
            UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, domainUserName);
            if (user == null)
                Enabled = null;
            else
                Enabled = user.Enabled;
            return Enabled;
        }
        public List<UserPrincipal> GetADUserListByActiveStatusBy(bool enabled)
        {
            List<UserPrincipal> UserPrincipals = new List<UserPrincipal>();
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain);
                UserPrincipal user = new UserPrincipal(context);
                user.Enabled = enabled;

                PrincipalSearcher srch = new PrincipalSearcher(user);
                foreach (var found in srch.FindAll())
                {
                    UserPrincipal foundUser = found as UserPrincipal;
                    if (foundUser != null)
                    {
                        if (foundUser.EmployeeId != null)
                        {
                            UserPrincipals.Add(foundUser);
                        }
                    }
                }
                return UserPrincipals;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<ADUserProfile> GetADUserList()
        {
            List<ADUserProfile> AdUsers = new List<ADUserProfile>();

            try
            {
                Search.Filter = "(&(objectClass=user)(objectCategory=person))";
                Search.PropertiesToLoad.Add("samaccountname");
                Search.PropertiesToLoad.Add("mail");
                Search.PropertiesToLoad.Add("usergroup");
                Search.PropertiesToLoad.Add("givenName");
                SearchResult result;
                SearchResultCollection resultCol = Search.FindAll();
                if (resultCol != null)
                {
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        string UserNameEmailString = string.Empty;
                        result = resultCol[counter];
                        if (result.Properties.Contains("samaccountname") && result.Properties.Contains("mail") && result.Properties.Contains("givenName"))
                        {
                            ADUserProfile adUserProfile = new ADUserProfile();

                            adUserProfile.mail = (String)result.Properties["mail"][0];
                            adUserProfile.samAccountName = (String)result.Properties["samaccountname"][0];
                            adUserProfile.givenName = (String)result.Properties["givenName"][0];
                            AdUsers.Add(adUserProfile);
                        }
                    }
                }
                return AdUsers;
            }
            catch (Exception ex)
            {
                throw new BusinessException("ad异常！");
            }
        }
        public ADUserProfile GetADUserProfileByLoginName(string loginName)
        {
            ADUserProfile adUserProfile = new ADUserProfile();
            try
            {
                if (loginName != null && loginName.Length > 0) Search.Filter = string.Format("(&(objectCategory=person)(objectClass=user)(SAMAccountname={0}))", loginName);

                ResultSet = Search.FindOne();
                if (ResultSet != null)
                {
                    adUserProfile.samAccountName = GetProperty(ResultSet, "samAccountName");
                    adUserProfile.userPrincipalName = GetProperty(ResultSet, "userPrincipalName");
                    adUserProfile.cn = GetProperty(ResultSet, "cn");
                    adUserProfile.givenName = GetProperty(ResultSet, "givenName");
                    adUserProfile.initials = GetProperty(ResultSet, "initials");
                    adUserProfile.sn = GetProperty(ResultSet, "sn");
                    adUserProfile.title = GetProperty(ResultSet, "title");
                    adUserProfile.company = GetProperty(ResultSet, "company");
                    adUserProfile.homePostalAddress = GetProperty(ResultSet, "homePostalAddress");
                    adUserProfile.l = GetProperty(ResultSet, "l");
                    adUserProfile.st = GetProperty(ResultSet, "st");
                    adUserProfile.co = GetProperty(ResultSet, "co");
                    adUserProfile.postalCode = GetProperty(ResultSet, "postalCode");
                    adUserProfile.telephoneNumber = GetProperty(ResultSet, "telephoneNumber");
                    adUserProfile.otherTelephone = GetProperty(ResultSet, "otherTelephone");
                    adUserProfile.facsimileTelephoneNumber = GetProperty(ResultSet, "facsimileTelephoneNumber  ");
                    adUserProfile.mail = GetProperty(ResultSet, "mail");
                    adUserProfile.whenCreated = GetProperty(ResultSet, "whenCreated");
                    adUserProfile.whenChanged = GetProperty(ResultSet, "whenChanged");
                    adUserProfile.userAccountControl = Regex.IsMatch(GetProperty(ResultSet, "userAccountControl"), @"^\d+$") ? Convert.ToInt32(GetProperty(ResultSet, "userAccountControl")) : 0;
                    adUserProfile.ActiveUser = !Convert.ToBoolean(adUserProfile.userAccountControl & 0x0002);
                    adUserProfile.extensionAttribute1 = GetProperty(ResultSet, "extensionAttribute1");
                    adUserProfile.extensionAttribute2 = GetProperty(ResultSet, "extensionAttribute2");
                    adUserProfile.extensionAttribute3 = GetProperty(ResultSet, "extensionAttribute3");
                    adUserProfile.extensionAttribute4 = GetProperty(ResultSet, "extensionAttribute4");
                    adUserProfile.extensionAttribute5 = GetProperty(ResultSet, "extensionAttribute5");
                    adUserProfile.extensionAttribute6 = GetProperty(ResultSet, "extensionAttribute6");
                    adUserProfile.extensionAttribute7 = GetProperty(ResultSet, "extensionAttribute7");
                    adUserProfile.extensionAttribute8 = GetProperty(ResultSet, "extensionAttribute8");
                    adUserProfile.extensionAttribute9 = GetProperty(ResultSet, "extensionAttribute9");
                    adUserProfile.extensionAttribute10 = GetProperty(ResultSet, "extensionAttribute10");
                    adUserProfile.extensionAttribute11 = GetProperty(ResultSet, "extensionAttribute11");
                    adUserProfile.extensionAttribute12 = GetProperty(ResultSet, "extensionAttribute12");
                    adUserProfile.employeeID = GetProperty(ResultSet, "employeeID");
                    adUserProfile.displayName = GetProperty(ResultSet, "displayName");
                    adUserProfile.objectGUID = GetGuid(ResultSet, "ObjectGUID");
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException("ad异常！");
            }
            return adUserProfile;
        }


        public ADUserProfile GetADUserProfileByEmpId(string empId)
        {
            ADUserProfile adUserProfile = new ADUserProfile();
            try
            {
                if (empId != null && empId.Length > 0) Search.Filter = string.Format("(&(objectCategory=person)(objectClass=user)(employeeID={0}))", empId);

                ResultSet = Search.FindOne();
                if (ResultSet != null)
                {
                    adUserProfile.samAccountName = GetProperty(ResultSet, "samAccountName");
                    adUserProfile.userPrincipalName = GetProperty(ResultSet, "userPrincipalName");
                    adUserProfile.cn = GetProperty(ResultSet, "cn");
                    adUserProfile.givenName = GetProperty(ResultSet, "givenName");
                    adUserProfile.initials = GetProperty(ResultSet, "initials");
                    adUserProfile.sn = GetProperty(ResultSet, "sn");
                    adUserProfile.title = GetProperty(ResultSet, "title");
                    adUserProfile.company = GetProperty(ResultSet, "company");
                    adUserProfile.homePostalAddress = GetProperty(ResultSet, "homePostalAddress");
                    adUserProfile.l = GetProperty(ResultSet, "l");
                    adUserProfile.st = GetProperty(ResultSet, "st");
                    adUserProfile.co = GetProperty(ResultSet, "co");
                    adUserProfile.postalCode = GetProperty(ResultSet, "postalCode");
                    adUserProfile.telephoneNumber = GetProperty(ResultSet, "telephoneNumber");
                    adUserProfile.otherTelephone = GetProperty(ResultSet, "otherTelephone");
                    adUserProfile.facsimileTelephoneNumber = GetProperty(ResultSet, "facsimileTelephoneNumber  ");
                    adUserProfile.mail = GetProperty(ResultSet, "mail");
                    adUserProfile.whenCreated = GetProperty(ResultSet, "whenCreated");
                    adUserProfile.whenChanged = GetProperty(ResultSet, "whenChanged");
                    adUserProfile.userAccountControl = Regex.IsMatch(GetProperty(ResultSet, "userAccountControl"), @"^\d+$") ? Convert.ToInt32(GetProperty(ResultSet, "userAccountControl")) : 0;
                    adUserProfile.ActiveUser = !Convert.ToBoolean(adUserProfile.userAccountControl & 0x0002);
                    adUserProfile.extensionAttribute1 = GetProperty(ResultSet, "extensionAttribute1");
                    adUserProfile.extensionAttribute2 = GetProperty(ResultSet, "extensionAttribute2");
                    adUserProfile.extensionAttribute3 = GetProperty(ResultSet, "extensionAttribute3");
                    adUserProfile.extensionAttribute4 = GetProperty(ResultSet, "extensionAttribute4");
                    adUserProfile.extensionAttribute5 = GetProperty(ResultSet, "extensionAttribute5");
                    adUserProfile.extensionAttribute6 = GetProperty(ResultSet, "extensionAttribute6");
                    adUserProfile.extensionAttribute7 = GetProperty(ResultSet, "extensionAttribute7");
                    adUserProfile.extensionAttribute8 = GetProperty(ResultSet, "extensionAttribute8");
                    adUserProfile.extensionAttribute9 = GetProperty(ResultSet, "extensionAttribute9");
                    adUserProfile.extensionAttribute10 = GetProperty(ResultSet, "extensionAttribute10");
                    adUserProfile.extensionAttribute11 = GetProperty(ResultSet, "extensionAttribute11");
                    adUserProfile.extensionAttribute12 = GetProperty(ResultSet, "extensionAttribute12");
                    adUserProfile.employeeID = GetProperty(ResultSet, "employeeID");
                    adUserProfile.displayName = GetProperty(ResultSet, "displayName");
                    adUserProfile.objectGUID = GetGuid(ResultSet, "ObjectGUID");
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException("ad异常！");
            }
            return adUserProfile;
        }
        public ADUserProfile GetADUserProfileByObjectGuid(string objectGuid)
        {
            ADUserProfile adUserProfile = new ADUserProfile();
            try
            {
                if (objectGuid != null && objectGuid.Length > 0) Search.Filter = string.Format("(objectGuid={0})", objectGuid);

                ResultSet = Search.FindOne();
                if (ResultSet != null)
                {
                    adUserProfile.samAccountName = GetProperty(ResultSet, "samAccountName");
                    adUserProfile.userPrincipalName = GetProperty(ResultSet, "userPrincipalName");
                    adUserProfile.cn = GetProperty(ResultSet, "cn");
                    adUserProfile.givenName = GetProperty(ResultSet, "givenName");
                    adUserProfile.initials = GetProperty(ResultSet, "initials");
                    adUserProfile.sn = GetProperty(ResultSet, "sn");
                    adUserProfile.title = GetProperty(ResultSet, "title");
                    adUserProfile.company = GetProperty(ResultSet, "company");
                    adUserProfile.homePostalAddress = GetProperty(ResultSet, "homePostalAddress");
                    adUserProfile.l = GetProperty(ResultSet, "l");
                    adUserProfile.st = GetProperty(ResultSet, "st");
                    adUserProfile.co = GetProperty(ResultSet, "co");
                    adUserProfile.postalCode = GetProperty(ResultSet, "postalCode");
                    adUserProfile.telephoneNumber = GetProperty(ResultSet, "telephoneNumber");
                    adUserProfile.otherTelephone = GetProperty(ResultSet, "otherTelephone");
                    adUserProfile.facsimileTelephoneNumber = GetProperty(ResultSet, "facsimileTelephoneNumber  ");
                    adUserProfile.mail = GetProperty(ResultSet, "mail");
                    adUserProfile.whenCreated = GetProperty(ResultSet, "whenCreated");
                    adUserProfile.whenChanged = GetProperty(ResultSet, "whenChanged");
                    adUserProfile.userAccountControl = Regex.IsMatch(GetProperty(ResultSet, "userAccountControl"), @"^\d+$") ? Convert.ToInt32(GetProperty(ResultSet, "userAccountControl")) : 0;
                    adUserProfile.ActiveUser = !Convert.ToBoolean(adUserProfile.userAccountControl & 0x0002);
                    adUserProfile.extensionAttribute1 = GetProperty(ResultSet, "extensionAttribute1");
                    adUserProfile.extensionAttribute2 = GetProperty(ResultSet, "extensionAttribute2");
                    adUserProfile.extensionAttribute3 = GetProperty(ResultSet, "extensionAttribute3");
                    adUserProfile.extensionAttribute4 = GetProperty(ResultSet, "extensionAttribute4");
                    adUserProfile.extensionAttribute5 = GetProperty(ResultSet, "extensionAttribute5");
                    adUserProfile.extensionAttribute6 = GetProperty(ResultSet, "extensionAttribute6");
                    adUserProfile.extensionAttribute7 = GetProperty(ResultSet, "extensionAttribute7");
                    adUserProfile.extensionAttribute8 = GetProperty(ResultSet, "extensionAttribute8");
                    adUserProfile.extensionAttribute9 = GetProperty(ResultSet, "extensionAttribute9");
                    adUserProfile.extensionAttribute10 = GetProperty(ResultSet, "extensionAttribute10");
                    adUserProfile.extensionAttribute11 = GetProperty(ResultSet, "extensionAttribute11");
                    adUserProfile.extensionAttribute12 = GetProperty(ResultSet, "extensionAttribute12");
                    adUserProfile.objectGUID = GetGuid(ResultSet, "ObjectGUID");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return adUserProfile;
        }
        private static string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        private static string GetGuid(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                byte[] binaryData = searchResult.Properties[PropertyName][0] as byte[];
                //string strHex = BitConverter.ToString(binaryData);
                //Guid ObjectGUID = new Guid(strHex.Replace("-", ""));

                var ObjectGUID = new Guid(binaryData).ToString();

                return ObjectGUID.ToString();
            }
            else
            {
                return String.Empty;
            }
        }
    }

    public class ADUserProfile
    {
        public string samAccountName { get; set; }
        public string userPrincipalName { get; set; }
        public string cn { get; set; }
        public string givenName { get; set; }
        public string initials { get; set; }
        public string sn { get; set; }
        public string title { get; set; }
        public string company { get; set; }

        public string homePostalAddress { get; set; }
        public string l { get; set; }
        public string st { get; set; }
        public string co { get; set; }
        public string postalCode { get; set; }
        public string telephoneNumber { get; set; }
        public string otherTelephone { get; set; }
        public string facsimileTelephoneNumber { get; set; }
        public string mail { get; set; }

        public string whenCreated { get; set; }
        public string whenChanged { get; set; }
        public int userAccountControl { get; set; }
        public bool ActiveUser { get; set; }

        public string extensionAttribute1 { get; set; }
        public string extensionAttribute2 { get; set; }
        public string extensionAttribute3 { get; set; }
        public string extensionAttribute4 { get; set; }
        public string extensionAttribute5 { get; set; }
        public string extensionAttribute6 { get; set; }
        public string extensionAttribute7 { get; set; }
        public string extensionAttribute8 { get; set; }
        public string extensionAttribute9 { get; set; }
        public string extensionAttribute10 { get; set; }
        public string extensionAttribute11 { get; set; }
        public string extensionAttribute12 { get; set; }
        public string objectGUID { get; set; }
        public string employeeID { get; set; }
        public string displayName { get; set; }
    }

}
