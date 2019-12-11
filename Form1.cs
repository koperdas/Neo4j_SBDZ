using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Neo4jClient;
using Neo4jClient.Cypher;
using Neo4j.Driver.V1;
using Neo4jClient.Transactions;
using Newtonsoft.Json.Serialization;

namespace Neo4j_SBDZ
{
    public partial class Form1 : Form
    {
        private IDriver driver;
        public Form1()
        {
            InitializeComponent();

            driver = GraphDatabase.Driver(@"bolt://localhost:7687", AuthTokens.Basic("neo4j", "1111"));
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ISession session = driver.Session();

            session.CloseAsync();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("country", textBox1.Text);
            values.Add("city", textBox2.Text);
            values.Add("price", Convert.ToInt32(textBox3.Text));

            using (var session = driver.Session())
            {
                var result = session.WriteTransaction(transaction =>
                {
                    var _result = transaction.Run("CREATE (ee:Travel {country : $country, city : $city, price : $price})", values);
                    return _result;
                });
                MessageBox.Show("add");
            }
        }

        private void ShowList()
        {
            List<Travel> travelList = new List<Travel>();
            using (var session = driver.Session())
            {
                var result = session.WriteTransaction(transaction =>
                {
                    var _result = transaction.Run("MATCH (ee:Travel) RETURN ee");
                    return _result;
                });
                DataTable dt = new DataTable();
                Travel tmp = new Travel();
                dt.Columns.Add("country");
                dt.Columns.Add("city");
                dt.Columns.Add("price");
                foreach (IRecord record in result)
                {
                    foreach (var pair in record.Values)
                    {
                        INode node = pair.Value as INode;
                        if (node.Properties.Keys.Contains("country"))
                            tmp.Country = node.Properties["country"].ToString();
                        if (node.Properties.Keys.Contains("city"))
                            tmp.City = node.Properties["city"].ToString();
                        if (node.Properties.Keys.Contains("price"))
                            tmp.Price = Convert.ToInt32(node.Properties["price"]);

                        travelList.Add(tmp);

                        var row = dt.NewRow();

                        row[0] = tmp.Country;
                        row[1] = tmp.City;
                        row[2] = tmp.Price;
                        dt.Rows.Add(row);
                    }
                }
                dataGridView1.DataSource = dt;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("name", textBox6.Text);
            values.Add("surname", textBox5.Text);
            values.Add("age", Convert.ToInt32(textBox4.Text));

            using (var session = driver.Session())
            {
                var result = session.WriteTransaction(transaction =>
                {
                    var _result = transaction.Run("CREATE (bb:Company {name : $name, surname : $surname, age : $age})", values);
                    return _result;
                });
                MessageBox.Show("add");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Travel> travelList = new List<Travel>();
            using (var session = driver.Session())
            {
                var result = session.WriteTransaction(transaction =>
                {
                    var _result = transaction.Run("MATCH (ee:Travel) WHERE " + textBox9.Text + " " + textBox8.Text + " " + textBox7.Text + " RETURN ee");
                    return _result;
                });
                DataTable dt = new DataTable();
                Travel tmp = new Travel();
                dt.Columns.Add("country");
                dt.Columns.Add("city");
                dt.Columns.Add("price");
                foreach (IRecord record in result)
                {
                    foreach (var pair in record.Values)
                    {
                        INode node = pair.Value as INode;
                        if (node.Properties.Keys.Contains("country"))
                            tmp.Country = node.Properties["country"].ToString();
                        if (node.Properties.Keys.Contains("city"))
                            tmp.City = node.Properties["city"].ToString();
                        if (node.Properties.Keys.Contains("price"))
                            tmp.Price = Convert.ToInt32(node.Properties["price"]);

                        travelList.Add(tmp);

                        var row = dt.NewRow();

                        row[0] = tmp.Country;
                        row[1] = tmp.City;
                        row[2] = tmp.Price;
                        dt.Rows.Add(row);
                    }
                }
                dataGridView1.DataSource = dt;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowList();
        }
    }
}