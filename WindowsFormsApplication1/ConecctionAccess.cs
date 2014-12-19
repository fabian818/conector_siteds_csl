using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{


    public class ConecctionAccess
    {
        static string CadenaConexion = "";
        static OleDbConnection Conex;

        public static void ConectarSiteds()
        {
            CadenaConexion = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\prueba\sitedscliente.mdb";
            Conex = new OleDbConnection(CadenaConexion);
            Conex.Open();                      
        }

        public static void ConectarEpslog()
        {
            CadenaConexion = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=N:\epslog.mdb";
            Conex = new OleDbConnection(CadenaConexion);
            Conex.Open();
        }

        public static void ConectarTedef()
        {
            CadenaConexion = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\prueba\SSTD_EV.mdb";
            Conex = new OleDbConnection(CadenaConexion);
            Conex.Open();
        }

        public static void ConectarTarifario()
        {
            CadenaConexion = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\prueba\TARIFARIO.mdb";
            Conex = new OleDbConnection(CadenaConexion);
            Conex.Open();
        }

        public static void InsertMysql()
        {
            InsertMysqlProductPacific();
            /*
             * 
             * InsertMysqlInsuredAutorization();
            InsertMysqlInsuredCoverage();
            InsertMysqlAutorization();
            InsertMysqlCoverage();
             * 
             * 
            InsertMysqlDoctor();
            InsertMysqlDiagnosticCategory();
            InsertMysqlDiagnostic();            
            InsertMysqlDigemid();
            InsertMysqlSector();            
            InsertMysqlEan();ctos
            InsertMysqlAfiliationType();
            InsertMysqlRelationShip();
            InsertMysqlInsurance();
            InsertMysqlMoney();
            InsertMysqlService();
            InsertMysqlPrice();
            InsertMysqlClasificationServiceType();
            InsertMysqlMechanismType();
            InsertMysqlSubMechanismType();
            InsertMysqlProduct();
            //InsertMysqlCumSunasaProduct();
            InsertMysqlCoverageType();
            InsertMysqlSubCoverageType();
            InsertMysqlDigemid();
            InsertMysqlAutorization();
            InsertMysqlCoverage();
             * */
        }

        public static void TimerInsertMysql()
        {
            InsertMysqlInsuredAutorization();
            InsertMysqlInsuredCoverage();
            InsertMysqlAutorization();
            InsertMysqlCoverage();
        }
        
        public static void InsertMysqlPrice()
        {
            ConectarTarifario();
            string query = "select * from tarifario where codigo <> 0 and flag = '0' order by codigo";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                if (!ServiceExists(reader))
                {
                    ConnectionMySQL.Connect();
                    ConnectionMySQL.InsertService(Helper.SuavizatingCode(reader.GetValue(0).ToString()),reader.GetString(1),"","");
                    ConnectionMySQL.Disconnect();
                }
                ConnectionMySQL.IncludePrice(reader.GetValue(0).ToString(), reader.GetValue(2).ToString(), Convert.ToInt16(reader.GetValue(3).ToString()));
                UpdateAfterInsert("tarifario", "codigo", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlDoctor()
        {
            ConectarTarifario();
            string query = "select * from medicos where flag = '0' and dni <> '0' order by dni";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertDoctor(reader.GetValue(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                UpdateAfterInsert("medicos", "dni", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlProduct()
        {
            ConectarTarifario();
            string query = "select * from productos where flag = '0' order by cod_prod";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertProduct(reader.GetValue(0).ToString(), reader.GetString(1));
                UpdateAfterInsert("productos", "cod_prod", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlProductPacific()
        {
            ConectarEpslog();
            string query = "select * from seguros_datosgenerales order by cAutoCode";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertProductPacific(reader.GetValue(20).ToString(), reader.GetValue(2).ToString());
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }


        public static void InsertMysqlEan()
        {
            ConectarTedef();
            string query = "select * from ts_ean13 where flag = '0' order by codigoean13";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertEan(reader.GetValue(0).ToString(), reader.GetString(2), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetValue(5).ToString(), reader.GetValue(6).ToString(), reader.GetValue(7).ToString(), reader.GetValue(8).ToString(), reader.GetValue(9).ToString(), reader.GetValue(10).ToString());
                UpdateAfterInsert("ts_ean13", "codigoean13", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlDigemid()
        {
            ConectarTedef();
            string query = "select * from ts_catalogodigemid where flag = '0' order by cod_prod";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertDigemid(reader.GetString(0), reader.GetString(1), reader.GetValue(2).ToString(), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetValue(6).ToString(), reader.GetValue(7).ToString(),reader.GetString(8), reader.GetString(9));
                UpdateAfterInsert("ts_catalogodigemid", "cod_prod", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlCumSunasaProduct()
        {
            ConectarTedef();
            string query = "select * from ts_producto where flag = '0' order by codigoproducto";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertCumSunasaProduct(reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(3).ToString(), reader.GetValue(10).ToString(), reader.GetValue(9).ToString(), reader.GetValue(8).ToString(), reader.GetValue(4).ToString(), reader.GetValue(5).ToString(), reader.GetValue(6).ToString(), reader.GetValue(11).ToString(), reader.GetValue(12).ToString());
                UpdateAfterInsert("ts_producto", "codigoproducto", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlSector()
        {
            ConectarTedef();
            string query = "select * from ts_rubro where flag = '0'";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertSector(reader.GetString(0), reader.GetString(1));
                UpdateAfterInsert("ts_rubro", "codigorubro", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlClasificationServiceType()
        {
            ConectarTedef();
            string query = "select * from ts_tipoclasificaciongasto where flag = '0' order by codigoclasificaciongasto;";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertClasificationServiceType(reader.GetString(0), reader.GetString(1));
                UpdateAfterInsert("ts_rubro", "codigoclasificaciongasto", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlMechanismType()
        {
            ConectarTedef();
            string query = "select * from ts_mecanismopago where flag = '0'";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertMechanismType(reader.GetString(0), reader.GetString(1));
                UpdateAfterInsert("ts_mecanismopago", "codigomecanismopago", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlSubMechanismType()
        {
            ConectarTedef();
            string query = "select * from ts_submecanismopago where flag = '0' order by codigosubmecanismopago";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertSubMechanismType(reader.GetString(0), reader.GetString(1),reader.GetString(2));
                UpdateAfterInsert("ts_submecanismopago", "codigosubmecanismopago", 1, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlDiagnosticCategory()
        {
            ConectarSiteds();
            string query = "select cCateCode,sCateDesc from CateDiagnostico where flag = '0' order by cCateCode";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertDiagnosticCategory(reader.GetString(0), reader.GetString(1));
                UpdateAfterInsert("CateDiagnostico", "cCateCode", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        

        public static void InsertMysqlService()
        {
            ConectarTedef();
            string query = "select * from ts_nomenclador where ruc = '20494306043' and flag = '0' order by codigo";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            while (reader.Read())
            {
                if (!CategoryServiceExists(reader))
                {
                    ConnectionMySQL.Connect();
                    ConnectionMySQL.InsertCategoryService(Helper.SplitPoints(reader.GetString(1)));
                    ConnectionMySQL.Disconnect();
                }
                if (!SubCategoryServiceExists(reader))
                {
                    ConnectionMySQL.Connect();
                    ConnectionMySQL.InsertSubCategoryService(Helper.SplitPoints(reader.GetString(1)));
                    ConnectionMySQL.Disconnect();
                }
                ConnectionMySQL.Connect();
                ConnectionMySQL.InsertService(Helper.SplitPoints(reader.GetString(1)),reader.GetString(2),reader.GetString(3),reader.GetString(4));
                ConnectionMySQL.Disconnect();
                UpdateAfterInsert("ts_nomenclador", "CODIGO", 1, reader);
            }
            Desconectar();
            FinalMessage();
        }

        public static bool CategoryServiceExists(OleDbDataReader reader)
        {
            string query = "select * from category_services where code = '" + Helper.GetCategory(Helper.SplitPoints(reader.GetString(1))) + "';";
            return Evalue(query);
        }

        public static bool SubCategoryServiceExists(OleDbDataReader reader)
        {
            string query = "select * from sub_category_services where code = '" + Helper.GetSubCategory(Helper.SplitPoints(reader.GetString(1))) + "';";
            return Evalue(query);
        }

        public static void InsertMysqlDiagnostic()
        {
            ConectarTedef();
            string query = "select * from TS_CIE10 where flag = '0' order by codigocie10";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertDiagnostic(reader.GetString(0), reader.GetString(1));
                UpdateAfterInsert("TS_CIE10", "codigocie10", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        } 
 

        public static void InsertMysqlCoverageType()
        {
            ConectarTedef();
            string query = "select * from  ts_tipocobertvinctipprest where flag = '0' order by codigotipocoberturaprest";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertCoverageType(reader.GetString(0), reader.GetString(1));
                UpdateAfterInsert("ts_tipocobertvinctipprest", "codigotipocoberturaprest", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlSubCoverageType()
        {
            ConectarTarifario();
            string query = "select * from sub_coverage_types where flag = '0' order by id";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertSubCoverageType(reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetValue(5).ToString());
                UpdateAfterInsert("sub_coverage_types", "id", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlCoverage()
        {
            ConectarEpslog();
            string query = "select * from Coberturas where flag = '0' and cautocode <> '0000' order by cautocode";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertCoverage(reader.GetValue(2).ToString(), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetValue(8).ToString(), reader.GetValue(9).ToString(), reader.GetValue(16).ToString());
                UpdateAfterInsert("Coberturas", "cAutoCode", 2, reader);
            }
            Desconectar();
            ConnectionMySQL.Disconnect();
        }
        public static void InsertMysqlInsuredCoverage()
        {
            ConectarEpslog();
            string query = "select * from seguros_coberturas where flag = '0' and cautocode <> '0000' order by cautocode";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertCoverageInsured(reader.GetValue(2).ToString(), reader.GetValue(4).ToString(), reader.GetValue(5).ToString(), reader.GetValue(9).ToString(), reader.GetValue(10).ToString(), "");
                UpdateAfterInsert("seguros_coberturas", "cautocode", 2, reader);
            }
            Desconectar();
            ConnectionMySQL.Disconnect();
        } 

        public static void InsertMysqlAutorization()
        {
            ConectarEpslog();
            string query = "select * from datosgenerales where flag = '0' and cautocode <> '0000'";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            while(reader.Read())
            {
                if (!CompanyExists(reader))
                {
                    ConnectionMySQL.Connect();
                    ConnectionMySQL.InsertCompany(reader.GetString(24), reader.GetString(17), reader.GetString(16), reader.GetString(18));
                    ConnectionMySQL.Disconnect();
                }
                if (!InsuredExists(reader))
                {
                    string clinic_history_code = GetClinicHistoryCode();
                    ConnectionMySQL.Connect();
                    ConnectionMySQL.InsertInsured(reader.GetString(27), reader.GetString(17), reader.GetString(0), reader.GetString(22), reader.GetString(1), reader.GetString(23), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), Helper.GetDate(reader.GetString(12)), reader.GetInt16(13).ToString(), reader.GetString(14).ToString(), Helper.GetDate(reader.GetString(19)), Helper.GetDate(reader.GetString(20)), Helper.GetDate(reader.GetString(21)), reader.GetString(29), "", clinic_history_code);
                    ConnectionMySQL.Disconnect();
                }
                string intern_code = GetInternCode();
                ConnectionMySQL.Connect();
                ConnectionMySQL.InsertAuthorization(reader.GetString(1), reader.GetString(25), reader.GetString(5), reader.GetString(2), Helper.GetDateTime(reader.GetString(3)), reader.GetString(8), reader.GetString(6), reader.GetString(7), Helper.GetDate(reader.GetString(12)), reader.GetValue(35).ToString(), intern_code);
                ConnectionMySQL.Disconnect();
                UpdateAfterInsert("DatosGenerales", "cAutoCode", 2, reader);
            }
            Desconectar();
        }

        public static void InsertMysqlInsuredAutorization()
        {
            ConectarEpslog();
            string query = "select * from seguros_datosgenerales where flag = '0' and cautocode <> '0000'";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            while (reader.Read())
            {
                if (!CompanyInsuredExists(reader))
                {
                    ConnectionMySQL.Connect();
                    //CAMBIAR EL COMPANY_RUC POR COMPANY_CODE
                    ConnectionMySQL.InsertCompanyInsured(reader.GetValue(24).ToString(), reader.GetValue(23).ToString(), reader.GetString(16), reader.GetString(25));
                    ConnectionMySQL.Disconnect();
                }
                if (!InsuredExists(reader))
                {
                    string clinic_history_code = GetClinicHistoryCode();
                    ConnectionMySQL.Connect();
                    ConnectionMySQL.InsertInsuredInsured("5", reader.GetString(24), reader.GetString(0), reader.GetString(32), reader.GetString(1), reader.GetString(35), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), Helper.GetDate(reader.GetString(12)), reader.GetInt16(13).ToString(), reader.GetString(14).ToString(), Helper.GetDate("19900101"), Helper.GetDate("19900101"), Helper.GetDate("19900101"), reader.GetString(41), "", clinic_history_code);
                    ConnectionMySQL.Disconnect();
                }
                string intern_code = GetInternCode();
                ConnectionMySQL.Connect();
                ConnectionMySQL.InsertAuthorization(reader.GetString(1), reader.GetString(25), reader.GetString(5), reader.GetString(2), Helper.GetDateTime(reader.GetString(3)), reader.GetString(8), reader.GetString(6), reader.GetString(7), Helper.GetDate(reader.GetString(12)), "99999",intern_code);
                ConnectionMySQL.Disconnect();
                UpdateAfterInsert("seguros_datosgenerales", "cAutoCode", 2, reader);
            }
            Desconectar();
        }

        public static bool AfiliationTypeExists(OleDbDataReader reader)
        {
            string query = "select * from afiliation_types where code = '" + reader.GetString(27) + "';";
            return Evalue(query);
        }

        public static bool CompanyExists(OleDbDataReader reader)
        {
            string query = "select * from companies where ruc = '"+ reader.GetString(17)+"';";
            return Evalue(query);
        }

        public static bool CompanyInsuredExists(OleDbDataReader reader)
        {
            string query = "select * from companies where number = '" + reader.GetString(24) + "';";
            return Evalue(query);
        }

        public static bool InsuredExists(OleDbDataReader reader)
        {
            string query = "select * from insureds where code = '" + reader.GetString(1) + "';";
            return Evalue(query);
        }

        public static bool ServiceExists(OleDbDataReader reader)
        {
            string query = "select * from services where code = '" + Helper.SuavizatingCode(reader.GetValue(0).ToString()) + "';";
            return Evalue(query);
        }

        public static string GetClinicHistoryCode()
        {
            ConnectionMySQL.Connect();
            string query = "select max(abs(clinic_history_code)) from patients";
            MySqlCommand command = new MySqlCommand(query, ConnectionMySQL.GetConnection());
            MySqlDataReader readerm = command.ExecuteReader();
            string clinic_history_code = "0";
            while (readerm.Read())
            {
                string valor = readerm.GetValue(0).ToString();
                if (valor == "0" || valor == null || valor == "" || Convert.ToInt16(valor) <= 5000)
                {
                    clinic_history_code = "5001";
                }
                else
                {
                    clinic_history_code = (Convert.ToInt16(valor) + 1).ToString();
                }
            }
            ConnectionMySQL.Disconnect();
            return clinic_history_code;
        }

        public static string GetInternCode()
        {
            ConnectionMySQL.Connect();
            string query = "select max(abs(intern_code)) from authorizations";
            MySqlCommand command = new MySqlCommand(query, ConnectionMySQL.GetConnection());
            MySqlDataReader readerm = command.ExecuteReader();
            string intern_code = "0";
            while (readerm.Read())
            {
                string valor = readerm.GetValue(0).ToString();
                if (valor == "0" || valor == null || valor == "" || Convert.ToInt16(valor) <= 15000)
                {
                    intern_code = "15001";
                }
                else
                {
                    intern_code = (Convert.ToInt16(valor) + 1).ToString();
                }
            }
            ConnectionMySQL.Disconnect();
            return intern_code;
        }

        public static bool Evalue(string query)
        {
            ConnectionMySQL.Connect();
            MySqlCommand command = new MySqlCommand(query, ConnectionMySQL.GetConnection());
            MySqlDataReader readerm = command.ExecuteReader();
            bool flag = false;
            while (readerm.Read())
            {
                flag = true;
            }
            ConnectionMySQL.Disconnect();
            return flag;
        }

        public static void InsertMysqlProcedureType()
        {
            ConectarSiteds();
            string query = "select Entidad.cEntiCode, TipoProcedimientosEspeciales.cProcCode, TipoProcedimientosEspeciales.sProcName from TipoProcedimientosEspeciales inner join Entidad  on TipoProcedimientosEspeciales.cEntiIden = Entidad.cEntiIden where TipoProcedimientosEspeciales.flag = '0' order by TipoProcedimientosEspeciales.cEntiIden";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();            
            while (reader.Read())
            {
                ConnectionMySQL.Connect();                
                ConnectionMySQL.InsertProcedureType(reader.GetString(1).ToString(), reader.GetString(2).ToString(), reader.GetString(0).ToString());
                UpdateAfterInsert("TipoProcedimientosEspeciales", "cProcCode", 1, reader);
                ConnectionMySQL.Disconnect();
            }
            Desconectar();
            FinalMessage();
        }

        public static string GetCode(string table, OleDbDataReader reader)
        {
            string query2 = "select * from "+table+" where cEntiIden = " + reader.GetInt16(0).ToString();
            OleDbCommand commandselect = new OleDbCommand(query2, Conex);
            OleDbDataReader readers = commandselect.ExecuteReader();
            string code = "";
            while(readers.Read())
            {
                code = readers.GetString(1).ToString();
            }
            readers.Close();
            return code;
        }

        public static void InsertMysqlMoney()
        {
            ConectarSiteds();
            string query = "select * from Moneda where flag = '0'";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertMoney(reader.GetString(0).ToString(), reader.GetString(2).ToString());
                UpdateAfterInsert("Moneda", "cMoneCode", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        } 

        public static void InsertMysqlInsurance()
        {
            ConectarSiteds();
            string query = "select * from Entidad where flag = '0'";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertInsurance(reader.GetString(1).ToString(), reader.GetString(2).ToString());
                UpdateAfterInsert("Entidad", "cEntiCode", 1, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlRelationShip()
        {
            ConectarSiteds();
            string query = "select * from Parentesco where flag = '0'";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertRelationShip(reader.GetString(0).ToString(), reader.GetString(1).ToString());
                UpdateAfterInsert("Parentesco", "cParnCode", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void InsertMysqlAfiliationType()
        {
            ConectarSiteds();
            string query = "select * from TipoAfiliacion where flag = '0' order by cAfilType";
            OleDbCommand commandselect = new OleDbCommand(query, Conex);
            OleDbDataReader reader = commandselect.ExecuteReader();
            ConnectionMySQL.Connect();
            while (reader.Read())
            {
                ConnectionMySQL.InsertAfiliationType(reader.GetString(0).ToString(), reader.GetString(1).ToString());
                UpdateAfterInsert("TipoAfiliacion", "cAfilType", 0, reader);
            }
            ConnectionMySQL.Disconnect();
            FinalMessage();
        }

        public static void FinalMessage()
        {
            MessageBox.Show("Data ingresada correctamente");
        }

        public static void UpdateAfterInsert(string table, string campcode, int numbercode, OleDbDataReader reader)
        {
            OleDbCommand commandupdate = new OleDbCommand();
            commandupdate.CommandText = GetUpdateQuery(table,campcode);
            commandupdate.Connection = Conex;
            commandupdate.Parameters.AddWithValue("@flag", 1);
            commandupdate.Parameters.AddWithValue("@"+campcode, reader.GetValue(numbercode).ToString());
            executeNonQuery(commandupdate);
        }

        public static string GetUpdateQuery(string table, string campcode)
        {
            return "UPDATE "+table+" SET flag = @flag where "+campcode+" = @"+campcode; 
        }

        public static void UpdateAfterInsert(string table, string campcode1, int numbercode1, string campcode2, int numbercode2, OleDbDataReader reader)
        {
            OleDbCommand commandupdate = new OleDbCommand();
            commandupdate.CommandText = GetUpdateQuery(table, campcode1,campcode2);
            commandupdate.Connection = Conex;
            commandupdate.Parameters.AddWithValue("@flag", 1);
            commandupdate.Parameters.AddWithValue("@" + campcode1, reader.GetString(numbercode1).ToString());
            commandupdate.Parameters.AddWithValue("@" + campcode2, reader.GetString(numbercode2).ToString());
            executeNonQuery(commandupdate);
        }

        public static string GetUpdateQuery(string table, string campcode1, string campcode2)
        {
            return "UPDATE " + table + " SET flag = @flag where " + campcode1 + " = @" + campcode1 + " and " + campcode2 + " = @" + campcode2;
        }

        public static void UpdateAfterInsert(string table, string campcode1, int numbercode1, string campcode2, int numbercode2, string campcode3, int numbercode3, OleDbDataReader reader)
        {
            OleDbCommand commandupdate = new OleDbCommand();
            commandupdate.CommandText = GetUpdateQuery(table, campcode1, campcode2, campcode3);
            commandupdate.Connection = Conex;
            commandupdate.Parameters.AddWithValue("@flag", 1);
            commandupdate.Parameters.AddWithValue("@" + campcode1, reader.GetString(numbercode1).ToString());
            commandupdate.Parameters.AddWithValue("@" + campcode2, reader.GetString(numbercode2).ToString());
            commandupdate.Parameters.AddWithValue("@" + campcode3, reader.GetString(numbercode3).ToString());
            executeNonQuery(commandupdate);
        }

        public static string GetUpdateQuery(string table, string campcode1, string campcode2, string campcode3)
        {
            return "UPDATE " + table + " SET flag = @flag where " + campcode1 + " = @" + campcode1 + " and " + campcode2 + " = @" + campcode2 + " and " + campcode3 + " = @" + campcode3;
        }

        public static void executeNonQuery(OleDbCommand command)
        {
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("El error es: " + e.Message);
            }

        }

        public static void Desconectar()
        {
            Conex.Close();
        }

        
    }
}
