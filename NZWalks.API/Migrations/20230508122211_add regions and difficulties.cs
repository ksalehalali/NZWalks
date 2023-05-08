using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class addregionsanddifficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10e405c3-a390-4094-be7c-91a565fd1829"), "Hard" },
                    { new Guid("772da223-096e-4dfd-b900-95e22c3de191"), "Easy" },
                    { new Guid("a8c77684-72f2-4e75-9d36-7c96451f03bb"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("696e4c25-7c3d-49c9-be38-38fb74c063c9"), "BHR", "Albahra", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.seiu1000.org%2Fpost%2Fimage-dimensions&psig=AOvVaw0INXBPxzDdt40oc8apK8LH&ust=1683634630836000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCIim5vPZ5f4CFQAAAAAdAAAAABAE" },
                    { new Guid("9188e603-975f-4aba-ba34-ff0bdd353d3e"), "AHMM", "Abo Hammam", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.seiu1000.org%2Fpost%2Fimage-dimensions&psig=AOvVaw0INXBPxzDdt40oc8apK8LH&ust=1683634630836000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCIim5vPZ5f4CFQAAAAAdAAAAABAE" },
                    { new Guid("ae8f0b88-3e84-4dde-a30e-e2a8e4634d87"), "GHR", "Gharaneej", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.seiu1000.org%2Fpost%2Fimage-dimensions&psig=AOvVaw0INXBPxzDdt40oc8apK8LH&ust=1683634630836000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCIim5vPZ5f4CFQAAAAAdAAAAABAE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("10e405c3-a390-4094-be7c-91a565fd1829"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("772da223-096e-4dfd-b900-95e22c3de191"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a8c77684-72f2-4e75-9d36-7c96451f03bb"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("696e4c25-7c3d-49c9-be38-38fb74c063c9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9188e603-975f-4aba-ba34-ff0bdd353d3e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ae8f0b88-3e84-4dde-a30e-e2a8e4634d87"));
        }
    }
}
