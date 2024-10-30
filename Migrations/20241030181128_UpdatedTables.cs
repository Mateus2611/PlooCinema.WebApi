using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlooCinema.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Rooms_RoomId",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Sessions",
                newName: "RoomsId");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Sessions",
                newName: "MoviesId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_RoomId",
                table: "Sessions",
                newName: "IX_Sessions_RoomsId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_MovieId",
                table: "Sessions",
                newName: "IX_Sessions_MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Movies_MoviesId",
                table: "Sessions",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Rooms_RoomsId",
                table: "Sessions",
                column: "RoomsId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Movies_MoviesId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Rooms_RoomsId",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "RoomsId",
                table: "Sessions",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "Sessions",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_RoomsId",
                table: "Sessions",
                newName: "IX_Sessions_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_MoviesId",
                table: "Sessions",
                newName: "IX_Sessions_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Rooms_RoomId",
                table: "Sessions",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
