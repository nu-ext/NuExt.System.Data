namespace System.Data.Tests;

[TestClass]
public class ExtensionsTests
{
    [TestMethod]
    public void Test1()
    {
        var table = new DataTable("Sample");
        table.Columns.Add("ID", typeof(long)).AutoIncrement = true;
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Description", typeof(string));
        table.Columns.Add("Int32Value", typeof(int));
        table.Columns.Add("Int32NullableValue", typeof(int));
        table.Columns.Add("BoolValue", typeof(int));
        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {
                table.Rows.Add(null, $"Name{i}", $"Description{i}", i, i, 1);
            }
            else
            {
                table.Rows.Add(null, $"Name{i}", null, i, null, 0);
            }
        }
        Assert.HasCount(10, table.Rows);
        var listTable = new List<(long Id, string Name, string? Description, int Int32Value, int? Int32NullableValue, bool BoolValue)>();
        foreach (DataRow row in table.Rows)
        {
            listTable.Add((
                row.GetInt64("Id"),
                row.GetString("Name"),
                row.GetNullableString("Description"),
                row.GetInt32("Int32Value"),
                row.GetNullableInt32("Int32NullableValue"),
                row.GetBoolean("BoolValue")
            ));
        }

        Assert.HasCount(table.Rows.Count, listTable);
        for (int i = 0; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];
            var (id, name, description, int32Value, int32NullableValue, boolValue) = listTable[i];
            Assert.AreEqual(id, row["Id"]);
            Assert.AreEqual(name, row["Name"]);
            Assert.AreEqual(description ?? (object)DBNull.Value, row["Description"]);
            Assert.AreEqual(int32Value, row["Int32Value"]);
            Assert.AreEqual(int32NullableValue ?? (object)DBNull.Value, row["Int32NullableValue"]);
            Assert.AreEqual(Convert.ToInt32(boolValue), row["BoolValue"]);
        }
    }
}