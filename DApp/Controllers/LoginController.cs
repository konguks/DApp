using System;
using System.Web.Http;
using System.Configuration;

using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using DApp.Models;


namespace DApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        

        public LoginController()
        {

        }

        [AllowAnonymous]
        //[Route("Getdet")]
        public JObject Getdet(string cid, string pwd)
        {
           
            if (cid == null)
                cid = "";
            if (pwd == null)
                pwd = "";

            string res = string.Empty;
            string qry = "Select Custid, Password from login_auth where Custid='" + cid + "' and status='Y';";
            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            string ms_custid = string.Empty;
            string ms_passwd = string.Empty;


            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(qry, con);
            try
            {
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ms_custid = dr.GetString(0);
                        ms_passwd = dr.GetString(1);
                    }
                }
                else
                    res = "Please enter valid Customer Id";


            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            finally
            {
                con.Close();
            }

            if (string.IsNullOrEmpty(ms_custid))
            {
                res = "Please enter valid Customer Id";
                //return JsonConvert.SerializeObject(lda);
            }
            else if (ms_custid == cid && pwd == ms_passwd)
            {
                res = "Success";
                //return JsonConvert.SerializeObject(lda);
            }
            else
                res = "Please Check your Password..";


            //string cid = "123";
            //if (cid == "123" && pwd == "abc")
            //{
            //    return "Success...";
            //}
            //return "working...";
            //res = "Failure";

            //JsonToken jso = new JsonToken();
            //jso.p
            //string jso = "{\"request\":{cid : \""+cid+"\",pwd : \""+pwd+"\"},\"response\":


            //LoginDisApp[] lda = new LoginDisApp[]
            //{
            //    new LoginDisApp.requestd = cid, pswd = pwd, response= res},
            //    new LoginDisApp { custid = cid, pswd = pwd, response= res}
            //};

            //LoginDisApp lda = new LoginDisApp();

            //lda.

            dynamic request = new JObject();

            request.cid = cid;
            request.pwd = pwd;

            dynamic response = new JObject();

            response.cid = ms_custid;
            response.pwd = ms_passwd;
            response.resp = res;

            //dynamic cwp = new
            //{
            //    Request = request,
            //    Response = response
            //};

            dynamic cwp = new JObject();
            cwp.Request = request;
            cwp.Response = response;


            //return JsonConvert.SerializeObject(cwp);
            return cwp;
        }
    }
}
