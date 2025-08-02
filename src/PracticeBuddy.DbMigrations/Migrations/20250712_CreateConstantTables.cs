using FluentMigrator;

namespace PracticeBuddy.DbMigrations.Migrations;

// [Migration(20250712)]
public class CreateConstantTables : AutoReversingMigration
{
    public override void Up()
    {
        // Create.Table("music_key")
        //     .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
        //     .WithColumn("key").AsString(100).NotNullable();
        // Insert.IntoTable("music_key").Row(new { key = "C♭" });
        // Insert.IntoTable("music_key").Row(new { key = "C" });
        // Insert.IntoTable("music_key").Row(new { key = "C#" });
        // Insert.IntoTable("music_key").Row(new { key = "D♭" });
        // Insert.IntoTable("music_key").Row(new { key = "D" });
        // Insert.IntoTable("music_key").Row(new { key = "D#" });
        // Insert.IntoTable("music_key").Row(new { key = "E♭" });
        // Insert.IntoTable("music_key").Row(new { key = "E" });
        // Insert.IntoTable("music_key").Row(new { key = "E#" });
        // Insert.IntoTable("music_key").Row(new { key = "F♭" });
        // Insert.IntoTable("music_key").Row(new { key = "F" });
        // Insert.IntoTable("music_key").Row(new { key = "F#" });
        // Insert.IntoTable("music_key").Row(new { key = "G♭" });
        // Insert.IntoTable("music_key").Row(new { key = "G" });
        // Insert.IntoTable("music_key").Row(new { key = "G#" });
        // Insert.IntoTable("music_key").Row(new { key = "A♭" });
        // Insert.IntoTable("music_key").Row(new { key = "A" });
        // Insert.IntoTable("music_key").Row(new { key = "A#" });
        // Insert.IntoTable("music_key").Row(new { key = "B♭" });
        // Insert.IntoTable("music_key").Row(new { key = "B" });
        // Insert.IntoTable("music_key").Row(new { key = "B#" });

        // Create.Table("exercise_modifier")
        //     .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
        //     .WithColumn("modifier").AsString(100).NotNullable();
        // Insert.IntoTable("exercise_modifier").Row(new { modifier = "stacatto" });
        // Insert.IntoTable("exercise_modifier").Row(new { modifier = "ascending" });
        // Insert.IntoTable("exercise_modifier").Row(new { modifier = "descending" });
        
        // Insert.IntoTable("exercise_modifier").Row(new { modifier = "contrarymotion" });
        // Insert.IntoTable("exercise_modifier").Row(new { modifier = "majorthirds" });

        // Create.Table("TestTable2")
        // 	.WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
        // 	.WithColumn("Name").AsString(255).Nullable()
        // 	.WithColumn("TestTableId").AsInt32().NotNullable();
        //  .WithColumn("goal_bpm").AsFixedLengthString(10).NotNullable()
        //  .WithColumn("Price").AsDecimal(18, 2).NotNullable()
        //  .WithColumn("DiscountPercentage").AsInt32().Nullable();

        // Create.Index("ix_Name").OnTable("TestTable2").OnColumn("Name").Ascending()
        // 	.WithOptions().NonClustered();

        // Create.Column("Name2").OnTable("TestTable2").AsBoolean().Nullable();

        // Create.ForeignKey("fk_TestTable2_TestTableId_TestTable_Id")
        // 	.FromTable("TestTable2").ForeignColumn("TestTableId")
        // 	.ToTable("TestTable").PrimaryColumn("Id");

        // Insert.IntoTable("TestTable").Row(new { Name = "Test" });
    }

	// public override void Down()
	// {
	// 	Delete.Table("music_keys");
	// 	Delete.Table("exercise_modifier");
	// }
}