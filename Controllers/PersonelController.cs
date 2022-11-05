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
    public class PersonelController : Controller
    {
        string connString = "DATA SOURCE = BARIS; INITIAL CATALOG = CagriMerkeziDB; USER ID = sa; PASSWORD = 123; Trusted_Connection = true;";
        string sorgu = "";

        [HttpGet("liste")]
        public IActionResult CagriListe(int id)
        {
            List<TBL_PERSONEL> cagriListe = new List<TBL_PERSONEL>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (id > 0)
                    sorgu = "select * from TBL_PERSONEL where ID =" + id;
                else
                    sorgu = "select * from TBL_PERSONEL";

                cagriListe = conn.Query<TBL_PERSONEL>(sorgu).ToList();
            }
            return Ok(cagriListe);
        }

        public int LoginKontrol(string gelenEmail, string gelenSifre)
        {
            string sorgu = "select * from TBL_PERSONEL where EMAIL ='" + gelenEmail + "' and PASSWORD ='" + gelenSifre + "'";
            SqlCommand cmd;
            SqlDataReader dr;

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            cmd = new SqlCommand(sorgu, conn);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                conn.Close();
                return 1;
            }
            else
            {
                conn.Close();
                return 0;
            }
        }



        [HttpGet("loginControl")]
        public IActionResult PersonelListelee(string email, string sifre)
        {
            string sorgu = "";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                sorgu = "SELECT * FROM TBL_PERSONEL";
                conn.Query<TBL_PERSONEL>(sorgu).ToList();
            }

            return Ok(LoginKontrol(email, sifre));
        }
    }
}
