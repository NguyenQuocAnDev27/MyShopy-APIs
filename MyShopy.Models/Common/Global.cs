using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyShopy.Models.Common
{
    public class Global
    {
        //Hàm lấy giá trị cột ID theo tên v
        public static int GetIDinDT(DataTable dt, int row, string v)
        {
            return dt == null || dt.Rows.Count == 0 || dt.Rows[row][v].ToString() == "" ? -1 : int.Parse(dt.Rows[row][v].ToString() ?? "");
        }

        //Hàm lấy giá trị cột ID theo vị trí cột v
        public static int GetIDinDT(DataTable dt, int row, int v)
        {
            return dt == null || dt.Rows.Count == 0 ? -1 : int.Parse(dt.Rows[row][v].ToString() ?? "");
        }

        //Lấy giá trị cột v
        public static string GetinDT_String(DataTable dt, int row, string v)
        {
            return dt == null || dt.Rows.Count == 0 ? "" : dt.Rows[row][v].ToString() ?? "";
        }

        //Hàm lấy giá trị tại vị trí tương ứng
        public static string GetinDT_String(DataTable dt, int row, int v)
        {
            return dt == null || dt.Rows.Count == 0 ? "" : dt.Rows[row][v].ToString() ?? "";
        }

        //Hàm đếm số lượng row trong table
        public static int DTCount(DataTable dsKey)
        {
            return dsKey == null || dsKey.Rows.Count == 0 ? 0 : dsKey.Rows.Count;
        }

        //Loai bo khoang trong thay bang dau "_"
        internal static string GetKeyJoin(string searchInput)
        {
            return string.Join("_", searchInput.Split(' ').ToList().Where(x => !string.IsNullOrEmpty(x)).ToArray());
        }

        // Custom DateTime Converter for JSON
        public class CustomDateTimeConverter : JsonConverter<DateTime>
        {
            private const string Format = "HH:mm:ss dd-MM-yyyy";

            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (DateTime.TryParse(reader.GetString(), out var date))
                {
                    return date;
                }
                throw new JsonException("Invalid date format");
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(Format));
            }
        }
    }
}
