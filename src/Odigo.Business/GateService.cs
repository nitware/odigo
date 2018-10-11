using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Business.Interfaces;
using Odigo.Model.Model;
using System.Linq.Expressions;

namespace Odigo.Business
{
    public class GateService : IGateService
    {
        private readonly IRepository _da;

        public GateService(IRepository da)
        {
            _da = da;
        }

        public LoginDetail Get(string userName, string password)
        {
            try
            {
                Func<PERSON_LOGIN, bool> selector = pl => pl.PERSON.Email.Equals(userName, StringComparison.OrdinalIgnoreCase);
                LoginDetail loginDetail = _da.GetModelBy<LoginDetail, PERSON_LOGIN>(selector);

                if (loginDetail != null)
                {
                    byte[] enteredPasswordHash = CreatePasswordHash(password);
                    if (ComparePassword(enteredPasswordHash, loginDetail.Password))
                    {
                        return loginDetail;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ChangePassword(Person person, string password)
        {
            try
            {
                bool passwordChanged = false;
                Func<PERSON_LOGIN, bool> predicate = c => c.Person_Id == person.Id;
                PERSON_LOGIN loginDetailEntity = _da.GetSingleBy<PERSON_LOGIN>(predicate);

                byte[] hash = CreatePasswordHash(password);
                loginDetailEntity.Password = hash;

                int rowsAffected = _da.Save();
                if (rowsAffected > 0)
                {
                    passwordChanged = true;
                }

                return passwordChanged;
            }
            //catch (NullReferenceException)
            //{
            //    throw new NullReferenceException(ArgumentNullException);
            //}
            //catch (UpdateException)
            //{
            //    throw new UpdateException(UpdateException);
            //}
            catch (Exception)
            {
                throw;
            }
        }

        public byte[] CreatePasswordHash(string password)
        {
            try
            {
                HashAlgorithm hashAlg = new SHA512Managed();
                byte[] pwordData = Encoding.Default.GetBytes(password);
                byte[] hash = hashAlg.ComputeHash(pwordData);
                return hash;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ComparePassword(byte[] hash, byte[] oldHash)
        {
            try
            {
                if (hash == null || oldHash == null || hash.Length != oldHash.Length)
                {
                    return false;
                }

                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != oldHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

       


        //public bool ResetPassword(LoginDetail loginDetail)
        //{
        //    try
        //    {
        //        Func<PERSON_LOGIN, bool> selector = l => l.Person_Id == loginDetail.Person.Id;
        //        PERSON_LOGIN loginEntity = GetEntityBy(selector);

        //        if (loginEntity != null)
        //        {
        //            byte[] hash = CreatePasswordHash(DefaultPassword);

        //            loginEntity.Password = hash;
        //            loginEntity.Is_Locked = false;
        //            loginEntity.Is_Activated = true;
        //            loginEntity.Is_First_Logon = true;

        //            int rowsAffected = repository.SaveChanges();
        //            if (rowsAffected > 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                throw new Exception(NoItemModified);
        //            }
        //        }
        //        else
        //        {
        //            return Add(loginDetail.Person) != null ? true : false;
        //        }
        //    }
        //    catch (NullReferenceException)
        //    {
        //        throw new NullReferenceException(ArgumentNullException);
        //    }
        //    catch (UpdateException)
        //    {
        //        throw new UpdateException(UpdateException);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}




    }
}
