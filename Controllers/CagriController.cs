using CagriMerkeziAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CagriMerkeziAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CagriController : Controller
    {
        string connString = "DATA SOURCE = BARIS; INITIAL CATALOG = CagriMerkeziDB; USER ID = sa; PASSWORD = 123; Trusted_Connection = true;";
        string sorgu = "";

        [HttpGet("liste")]
        public IActionResult CagriListe(int id)
        {
            List<TBL_CAGRI> cagriListe = new List<TBL_CAGRI>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (id > 0)
                    sorgu = "select * from TBL_CAGRI where ID =" + id;
                else
                    sorgu = "select * from TBL_CAGRI";

                cagriListe = conn.Query<TBL_CAGRI>(sorgu).ToList();
            }
            return Ok(cagriListe);
        }


        [HttpDelete("{id}")]
        public IActionResult CagriSil(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                sorgu = "DELETE FROM TBL_CAGRI WHERE ID =" + id;
                conn.Execute(sorgu);
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult CagriEkle(TBL_CAGRI cagri)
        {
            cagri.CALL_DATE = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                sorgu = "insert into TBL_CAGRI values(@PERSONEL_ID, @CUSTOMER_NAME, @CUSTOMER_PHONE, @SUBJECT, @DESCRIPTION, @PRICE, @CALL_DATE)";
                var prms = new
                {
                    PERSONEL_ID = cagri.PERSONEL_ID,
                    CUSTOMER_NAME = cagri.CUSTOMER_NAME,
                    CUSTOMER_PHONE = cagri.CUSTOMER_PHONE,
                    SUBJECT = cagri.SUBJECT,
                    DESCRIPTION = cagri.DESCRIPTION,
                    PRICE = cagri.PRICE,
                    CALL_DATE = cagri.CALL_DATE
                };

                conn.Execute(sorgu, prms);
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult CagriGuncelle(TBL_CAGRI cagri)
        {
            cagri.CALL_DATE = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                sorgu = "update TBL_CAGRI set PERSONEL_ID=@PERSONEL_ID, CUSTOMER_NAME=@CUSTOMER_NAME, CUSTOMER_PHONE=@CUSTOMER_PHONE, SUBJECT=@SUBJECT, DESCRIPTION=@DESCRIPTION," +
                    "PRICE=@PRICE, CALL_DATE=@CALL_DATE where ID=@cid";
                var prms = new
                {
                    cid = cagri.ID,
                    PERSONEL_ID = cagri.PERSONEL_ID,
                    CUSTOMER_NAME = cagri.CUSTOMER_NAME,
                    CUSTOMER_PHONE = cagri.CUSTOMER_PHONE,
                    SUBJECT = cagri.SUBJECT,
                    DESCRIPTION = cagri.DESCRIPTION,
                    PRICE = cagri.PRICE,
                    CALL_DATE = cagri.CALL_DATE
                };
                conn.Execute(sorgu, prms);
            }
            return Ok();
        }

        [HttpGet("ara")]
        public IActionResult Ara(string gelenIsim)
        {
            string sorgu = "";

            List<TBL_CAGRI> GorevList = new List<TBL_CAGRI>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                sorgu = "SELECT * FROM TBL_CAGRI WHERE CUSTOMER_NAME like '%" + gelenIsim + "%'";

                GorevList = conn.Query<TBL_CAGRI>(sorgu).ToList();
            }

            return Ok(GorevList);
        }
    }
}
