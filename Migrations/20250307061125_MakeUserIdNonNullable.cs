using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    public partial class MakeUserIdNonNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 先更新現有資料，將 null 的 UserId 設定為預設使用者ID
            // 如果外鍵已存在，先刪除它
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_AspNetUsers_UserId",
                table: "ToDos");

            // 先更新資料以確保所有 UserId 都有有效值（例如）
            migrationBuilder.Sql("UPDATE ToDos SET UserId = '432325b1-0a17-41cc-993b-d33bb6e91c94' WHERE UserId IS NULL");

            // 修改 UserId 欄位為非空
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ToDos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            // 重新新增外鍵約束
            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_AspNetUsers_UserId",
                table: "ToDos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_AspNetUsers_UserId",
                table: "ToDos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ToDos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // 可選：如果之前在 Up() 中更新了資料，可以考慮還原（視需求而定）
        }
    }
}
