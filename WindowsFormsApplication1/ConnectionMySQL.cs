using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    class ConnectionMySQL
    {
        private static MySqlConnection Conex;

        public static MySqlConnection GetConnection()
        {
            return Conex;
        }
        public static void Connect()
        {
            Conex = new MySqlConnection("server=127.0.0.1; database=csl_development; Uid=root; pwd=root; port=3306;default command timeout=3600");

            Conex.Open();
        }

        public static void Disconnect()
        {
            Conex.Close();
        }

        public static void InsertCumSunasaProduct(string code, string name, string form, string owner, string manufacturer, string report_unity, string measure, string measure_unity, string posologic_unity, string atc_code, string atc_name)
        {
            try
            {
                string query = "INSERT INTO cum_sunasa_products (code,name,form,owner,manufacturer,report_unity,measure,measure_unity,posologic_unity,atc_code,atc_name) VALUES (@code,@name,@form,@owner,@manufacturer,@report_unity,@measure,@measure_unity,@posologic_unity,@atc_code,@atc_name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@form", form);
                command.Parameters.AddWithValue("@owner", owner);
                command.Parameters.AddWithValue("@manufacturer", manufacturer);
                command.Parameters.AddWithValue("@report_unity", report_unity);
                command.Parameters.AddWithValue("@measure", measure);
                command.Parameters.AddWithValue("@measure_unity", measure_unity);
                command.Parameters.AddWithValue("@posologic_unity", posologic_unity);
                command.Parameters.AddWithValue("@atc_code", atc_code);
                command.Parameters.AddWithValue("@atc_name", atc_name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertEan(string code, string name, string concentration, string form, string form_simplificated, string presentation, string fractions, string due_date_sanitary, string number_sanitary, string holder_name)
        {
            try
            {
                string query = "INSERT INTO ean_products (code,name,concentration,form,form_simplificated,presentation,due_date_sanitary,number_sanitary,holder_name) VALUES (@code,@name,@concentration,@form,@form_simplificated,@presentation,@due_date_sanitary,@number_sanitary,@holder_name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@concentration", concentration);
                command.Parameters.AddWithValue("@form", form);
                command.Parameters.AddWithValue("@form_simplificated", form_simplificated);
                command.Parameters.AddWithValue("@presentation", presentation);
                command.Parameters.AddWithValue("@due_date_sanitary", Helper.GetCorrectDate(due_date_sanitary));
                command.Parameters.AddWithValue("@number_sanitary", number_sanitary);
                command.Parameters.AddWithValue("@holder_name", holder_name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertDigemid(string code, string name, string concentration, string form, string form_simplificated, string presentation, string fractions, string due_date_sanitary, string sanitary_number, string holder_name)
        {
            try
            {
                string query = "INSERT INTO digemid_products (code,name,concentration,form,form_simplificated,presentation,due_date_sanitary,sanitary_number,holder_name) VALUES (@code,@name,@concentration,@form,@form_simplificated,@presentation,@due_date_sanitary,@sanitary_number,@holder_name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@concentration", concentration);
                command.Parameters.AddWithValue("@form", form);
                command.Parameters.AddWithValue("@form_simplificated", form_simplificated);
                command.Parameters.AddWithValue("@presentation", presentation);
                command.Parameters.AddWithValue("@due_date_sanitary", Helper.GetCorrectDate(due_date_sanitary));
                command.Parameters.AddWithValue("@sanitary_number", sanitary_number);
                command.Parameters.AddWithValue("@holder_name", holder_name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void IncludePrice(string code, string unitary, int id)
        {
            try
            {
                code = Helper.SuavizatingCode(code);
                string query = "UPDATE services set unitary = @unitary  where code = @code;";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@unitary", Convert.ToDecimal(unitary));
                RelationPossession(id, code);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void RelationPossession(int clinic_area_id, string code)
        {
            try
            {
                Conex.Open();
                string query = "update services set clinic_area_id = @clinic_area_id where code = @code;";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@clinic_area_id", clinic_area_id);
                command.Parameters.AddWithValue("@code", code);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }



        public static void InsertDoctor(string code, string complet_name, string tuition_code, string speciality_name)
        {
            try
            {
                string query = "INSERT INTO doctors (document_identity_code,complet_name, tuition_code, specialty_name,document_identity_type_id) VALUES (@dni,@complet_name,@tuition_code,@speciality_name,1);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@dni", code);
                command.Parameters.AddWithValue("@complet_name", complet_name);
                command.Parameters.AddWithValue("@tuition_code", tuition_code);
                command.Parameters.AddWithValue("@speciality_name", speciality_name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertSector(string code, string name)
        {
            try
            {
                string query = "INSERT INTO sectors (code,name) VALUES (@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertClasificationServiceType(string code, string name)
        {
            try
            {
                string query = "INSERT INTO clasification_service_types (code,name) VALUES (@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertMechanismType(string code, string name)
        {
            try
            {
                string query = "INSERT INTO mechanism_payments (code,name) VALUES (@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertSubMechanismType(string mechanism_payment_code, string code, string name)
        {
            try
            {
                int mechanism_payment_id = GetIdFromCode("mechanism_payments", mechanism_payment_code);
                string query = "INSERT INTO sub_mechanism_pay_types(mechanism_payment_id,code,name) VALUES(@mechanism_payment_id,@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@mechanism_payment_id", mechanism_payment_id);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }   

        public static void InsertCategoryService(string code)
        {
            try
            {
                string query = "INSERT INTO category_services (code) VALUES (@code);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", Helper.GetCategory(code));
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertSubCategoryService(string code)
        {
            try
            {
                int category_service_id = GetIdFromCode("category_services", Helper.GetCategory(code));
                string query = "INSERT INTO sub_category_services (category_service_id,code) VALUES (@category_service_id,@code);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@category_service_id", category_service_id);
                command.Parameters.AddWithValue("@code", Helper.GetSubCategory(code));
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertService(string code, string name, string conta_code, string conta_name)
        {
            try
            {
                int sub_category_service_id = GetIdFromCode("sub_category_services", Helper.GetSubCategory(code));
                string query = "INSERT INTO services (sub_category_service_id,code,name,contable_code,contable_name) VALUES (@sub_category_service_id,@code,@name,@contable_code,@contable_name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@sub_category_service_id", sub_category_service_id);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@contable_code", conta_code);
                command.Parameters.AddWithValue("@contable_name", conta_name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertSpecialProcedure(string authorization_code, string coverage_type_code, string procedure_type_code, string deductible, string percentage_coverade)
        {
            try
            {
                int authorization_id = GetIdFromCode("authorizations", authorization_code);
                int coverage_type_id = GetIdFromCode("coverage_types", coverage_type_code);
                int procedure_type_id = GetIdFromCode("procedure_types", procedure_type_code);
                string query = "INSERT INTO special_procedures (authorization_id,coverage_type_id,procedure_type_id,deductible,percentage_coverade) VALUES (@authorization_id,@coverage_type_id,@procedure_type_id,@deductible,@percentage_coverade);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@authorization_id", authorization_id);
                command.Parameters.AddWithValue("@coverage_type_id", coverage_type_id);
                command.Parameters.AddWithValue("@procedure_type_id", procedure_type_id);
                command.Parameters.AddWithValue("@deductible", Convert.ToDecimal(deductible));
                command.Parameters.AddWithValue("@percentage_coverade", Convert.ToDecimal(percentage_coverade));
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertExclusion(string authorization_code, string diagnostic_code, string observation)
        {
            try
            {
                int authorization_id = GetIdFromCode("authorizations", authorization_code);
                int diagnostic_id = GetIdFromCode("diagnostics", diagnostic_code);
                string query = "INSERT INTO exclusions (authorization_id,diagnostic_id,observation) VALUES (@authorization_id,@diagnostic_id,@observation);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@authorization_id", authorization_id);
                command.Parameters.AddWithValue("@diagnostic_id", diagnostic_id);
                command.Parameters.AddWithValue("@observation", observation);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertCoverage(string authorization_code, string code, string name, string cop_fijo, string cop_var, string cop_text)
        {
            try
            {
                int authorization_id = GetIdFromCode("authorizations", authorization_code);
                int sub_coverage_type_id = GetIdFromCode("sub_coverage_types", code);
                string query = "INSERT INTO coverages (authorization_id,code,sub_coverage_type_id,name,cop_fijo,cop_var,cop_text) VALUES (@authorization_id,@code,@sub_coverage_type_id,@name,@cop_fijo,@cop_var,@cop_text);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@authorization_id", authorization_id);
                command.Parameters.AddWithValue("@sub_coverage_type_id", sub_coverage_type_id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@cop_fijo", Convert.ToDecimal(cop_fijo));
                command.Parameters.AddWithValue("@cop_var", Convert.ToDecimal(cop_var));
                command.Parameters.AddWithValue("@cop_text", cop_text);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }
        
        public static void InsertDiagnostic(string code, string name)
        {
            try
            {
                int diagnostic_category_id = GetIdFromCode("diagnostic_categories", code.Substring(0,3));
                string query = "INSERT INTO diagnostic_types (diagnostic_category_id,code,name) VALUES (@diagnostic_category_id,@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@diagnostic_category_id", diagnostic_category_id);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertDiagnosticCategory(string code, string name)
        {
            try
            {
                string query = "INSERT INTO diagnostic_categories (code,name) VALUES (@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertService(string code, string name)
        {
            try
            {
                string query = "INSERT INTO services (code,name) VALUES (@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

         
        public static void InsertSubCoverageType(string coverage_type_code, string fact_code, string name, string code, string other_code)
        {
            try
            {
                int coverage_type_id = GetIdFromCode("coverage_types", coverage_type_code);
                string query = "INSERT INTO sub_coverage_types (code,other_code,name, fact_code, coverage_type_id) VALUES (@code,@other_code,@name,@fact_code,@coverage_type_id);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@other_code", other_code);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@fact_code", fact_code);
                command.Parameters.AddWithValue("@coverage_type_id", coverage_type_id);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertCoverageType(string code, string name)
        {
            try
            {
                string query = "INSERT INTO coverage_types (code,name) VALUES (@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertProduct(string code, string name)
        {
            try
            {
                string query = "INSERT INTO products (code,name) VALUES (@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@code", code);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertAuthorization(string insured_code, string money_code, string clinic_ruc, string code, string date, string name, string paternal, string maternal, string birthday, string product_code)
        {
            try
            {
                int product_id = GetIdFromCode("products", product_code);
                int patient_id = GetPatient(name, paternal,maternal,birthday);
                int money_id = GetIdFromCode("money", money_code);
                string query = "INSERT INTO authorizations (product_id,patient_id,money_id,clinic_id,code,date) VALUES (@product_id,@patient_id,@money_id,1,@code,@date);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@product_id", product_id);
                command.Parameters.AddWithValue("@patient_id", patient_id);
                command.Parameters.AddWithValue("@money_id", money_id);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@date", date);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertInsured(string afiliation_code, string company_ruc, string insurance_code, string relationship_code, string code, string dni, string paternal, string maternal, string name, string hold_paternal, string hold_maternal, string hold_name, string birthday, string age, string sex, string validity_i, string validity_f, string inclusion_date, string card_number, string observation)
        {
            try
            {   
                int relation_ship_id = GetIdFromCode("relation_ships", relationship_code);
                int afiliation_type_id = GetIdFromCode("afiliation_types", afiliation_code);
                int company_id = GetIdFromCode("companies", company_ruc, "ruc");
                int insurance_id = GetIdFromCode("insurances", insurance_code);
                int patient_id = InsertPatient(dni,name,paternal,maternal,birthday,age,sex);
                string query = "INSERT INTO insureds (afiliation_type_id,company_id,insurance_id,patient_id,code,hold_paternal,hold_maternal,hold_name,validity_i,validity_f,inclusion_date,relation_ship_id,card_number) VALUES (@afiliation_type_id,@company_id,@insurance_id,@patient_id,@code,@hold_paternal,@hold_maternal,@hold_name,@validity_i,@validity_f,@inclusion_date,@relation_ship_id,@card_number);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@afiliation_type_id", afiliation_type_id);
                command.Parameters.AddWithValue("@company_id", company_id);
                command.Parameters.AddWithValue("@insurance_id", insurance_id);
                command.Parameters.AddWithValue("@relation_ship_id", relation_ship_id);
                command.Parameters.AddWithValue("@patient_id", patient_id);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@hold_paternal", hold_paternal);
                command.Parameters.AddWithValue("@hold_maternal", hold_maternal);
                command.Parameters.AddWithValue("@hold_name", hold_name);
                command.Parameters.AddWithValue("@validity_i", validity_i);
                command.Parameters.AddWithValue("@validity_f", validity_f);
                command.Parameters.AddWithValue("@inclusion_date", inclusion_date);
                command.Parameters.AddWithValue("@card_number", card_number);              
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static int InsertPatient(string dni, string name, string paternal, string maternal, string birthday, string age, string sex)
        {
            try
            {
                string query = "INSERT INTO patients (document_identity_type_id,document_identity_code,name,paternal,maternal,birthday,age,sex,is_insured) VALUES(1,@dni,@name,@paternal,@maternal,@birthday,@age,@sex,@is_insured);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@dni", dni);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@paternal", paternal);
                command.Parameters.AddWithValue("@maternal", maternal);
                command.Parameters.AddWithValue("@birthday", birthday);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@sex", sex); ;
                command.Parameters.AddWithValue("@is_insured", true);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
                string query_select = "select id from patients where name= @name and paternal = @paternal and maternal = @maternal and birthday = @birthday;";
                MySqlCommand commandselect = new MySqlCommand(query_select, Conex);
                commandselect.Parameters.AddWithValue("@name", name);
                commandselect.Parameters.AddWithValue("@paternal", paternal);
                commandselect.Parameters.AddWithValue("@maternal", maternal);
                commandselect.Parameters.AddWithValue("@birthday", birthday);
                MySqlDataReader reader = commandselect.ExecuteReader();
                int id = 0;
                while (reader.Read())
                {
                    id = Convert.ToInt16(reader.GetValue(0));
                }
                reader.Close();
                return id;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static int GetPatient(string name, string paternal, string maternal, string birthday)
        {
            try
            {
                string query_select = "select id from patients where name= @name and paternal = @paternal and maternal = @maternal and birthday = @birthday;";
                MySqlCommand commandselect = new MySqlCommand(query_select, Conex);
                commandselect.Parameters.AddWithValue("@name", name);
                commandselect.Parameters.AddWithValue("@paternal", paternal);
                commandselect.Parameters.AddWithValue("@maternal", maternal);
                commandselect.Parameters.AddWithValue("@birthday", birthday);
                MySqlDataReader reader = commandselect.ExecuteReader();
                int id = 0;
                while (reader.Read())
                {
                    id = Convert.ToInt16(reader.GetValue(0));
                }
                reader.Close();
                return id;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertCompany(string number, string ruc, string plan, string name)
        {
            try
            {
                string query = "INSERT INTO companies (number,ruc,plan,name) VALUES (@number,@ruc,@plan,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@number", number);
                command.Parameters.AddWithValue("@ruc", ruc);
                command.Parameters.AddWithValue("@plan", plan);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

         
        public static void InsertProcedureType(string code, string description, string enticode)
        {
            try
            {
                int insurance_id = GetIdFromCode("insurances", enticode);
                string query = "INSERT INTO procedure_types (insurance_id,code,description) VALUES (@insurance_id,@code,@description);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@insurance_id", insurance_id);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@description", description);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();                
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static int GetIdFromCode(string table, string code)
        {
            string query2 = "select * from "+ table +" where code = '" + code + "';";
            MySqlCommand command = new MySqlCommand(query2, Conex);
            MySqlDataReader reader = command.ExecuteReader();
            int id = 0;
            while (reader.Read())
            {
                id = reader.GetInt16(0);
            } 
            reader.Close();
            return id;
        }

        public static int GetIdFromCode(string table, string code, string column)
        {
            string query2 = "select * from " + table + " where "+column+" = '" + code + "';";
            MySqlCommand command = new MySqlCommand(query2, Conex);
            MySqlDataReader reader = command.ExecuteReader();
            int id = 0;
            while (reader.Read())
            {
                id = reader.GetInt16(0);
            }
            reader.Close();
            return id;
        }

        public static void InsertStatus(string code, string name)
        {
            try
            {
                string query = "INSERT INTO statuses (code,name) VALUES (@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertMoney(string code, string name)
        {
            try
            {
                string query = "INSERT INTO money (code,name) VALUES (@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertInsurance(string code, string name)
        {
            try
            {
                string query = "INSERT INTO insurances (code,name) VALUES (@code,@name);";  
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertRelationShip(string code, string name)
        {
            try
            {
                string query = "INSERT INTO relation_ships (code,name) VALUES (@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void InsertAfiliationType(string code, string name)
        {
            try
            {
                string query = "INSERT INTO afiliation_types (code,name) VALUES (@code,@name);";
                MySqlCommand command = new MySqlCommand(query, Conex);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@name", name);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Se genero el siguiente error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public static void Mostrar()
        {

        }
    }
}
