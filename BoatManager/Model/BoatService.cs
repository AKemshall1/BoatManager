using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BoatManager.Model
{
    class BoatService
    {
        private static List<Boat> boatList;

        public BoatService()
        {
            boatList = new List<Boat>();
            RefreshList();
        }

        public bool RefreshList()
        {
            boatList.Clear();

            using(SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                connection.Open();

                string cmdString = "SELECT id, faction, name, hull, level, dateMarried FROM marriedBoats";
                SqlCommand command = new SqlCommand(cmdString, connection);
                
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Boat tempBoat = new Boat();

                        tempBoat.Id = (int)reader.GetValue(0);
                        tempBoat.Faction = (string)reader.GetValue(1);
                        tempBoat.Name = (string)reader.GetValue(2);
                        tempBoat.Hull = (string)reader.GetValue(3);
                        tempBoat.Level = (int)reader.GetValue(4);
                        // tempBoat.Date = (DateTime)reader.GetValue(4);

                        boatList.Add(tempBoat);

                    }

                    connection.Close();
                    return true;
                }
            }
        }

        public List<Boat> GetAll()
        {
            RefreshList();
            return boatList;
        }


        //User enters values into the textboxes
        //Textboxes are bound to the boat class
        //INotify updates these values
        //Add the boat to the database by using the variables in boat object and binding int the SQL command to avoid injection
        //Refresh the data


        public bool Add(Boat inputBoat)
        {
            //1. Build the command
            string query = "INSERT INTO marriedBoats(faction, hull, name, level) VALUES (@newFaction, @newHull, @newName, @newLevel);";

            using(SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newFaction", inputBoat.Faction);
                    command.Parameters.AddWithValue("@newHull", inputBoat.Hull);
                    command.Parameters.AddWithValue("@newName", inputBoat.Name);
                    command.Parameters.AddWithValue("@newLevel", inputBoat.Level);
                    //command.Parameters.AddWithValue("@newDateMarried", inputBoat.Date);

                    command.ExecuteNonQuery();

                }
      
            }
            return true;
        }

        public bool Delete(int id)
        {
            //1. Build the command
            string query = "DELETE FROM marriedBoats WHERE id = @id;";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                     command.Parameters.AddWithValue("@id", id);
      
                    command.ExecuteNonQuery();

                }

   

            }

           
            return true;
        }


        public bool Update(Boat inputBoat)
        {
            int boatEditingIndex = 0;

            for(int i = 0; i < boatList.Count; i++)
            {
                if(boatList[i].Id == inputBoat.Id)
                {
                    boatEditingIndex = i;
                }
            }


            //Use the inputted ID as the key for which boat to update
            //User inputs into the fields they want to change
            //Any fields left blank left unchanged

            string query = "UPDATE marriedBoats SET name = @name, faction = @faction, hull = @hull, level = @level WHERE id = @id;";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", inputBoat.Id);

                    if (inputBoat.Name == String.Empty)
                        command.Parameters.AddWithValue("@name", boatList[boatEditingIndex].Name);
                    else
                        command.Parameters.AddWithValue("@name", inputBoat.Name);

                    if (inputBoat.Faction == String.Empty)
                        command.Parameters.AddWithValue("@faction", boatList[boatEditingIndex].Faction);
                    else
                        command.Parameters.AddWithValue("@faction", inputBoat.Faction);

                    if (inputBoat.Hull == String.Empty)
                        command.Parameters.AddWithValue("@hull", boatList[boatEditingIndex].Hull);
                    else
                        command.Parameters.AddWithValue("@hull", inputBoat.Hull);

                    if (inputBoat.Level == 0)
                        command.Parameters.AddWithValue("@level", boatList[boatEditingIndex].Level);
                    else
                        command.Parameters.AddWithValue("@level", inputBoat.Level);


                    command.ExecuteNonQuery();


                }
            }


            return true;
        }


    }
}
