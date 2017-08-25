namespace OMSIFYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        father = c.String(nullable: false),
                        gender = c.String(nullable: false),
                        blood = c.String(nullable: false),
                        datebirth = c.String(nullable: false),
                        phone = c.Int(nullable: false),
                        district = c.String(nullable: false),
                        nationality = c.String(nullable: false),
                        userId = c.String(),
                        email = c.String(nullable: false),
                        CNIC = c.Int(nullable: false),
                        noper = c.String(nullable: false),
                        personcnic = c.String(nullable: false),
                        Adddress = c.String(nullable: false),
                        imgUrl = c.String(nullable: false),
                        password = c.String(),
                        logCont = c.Int(nullable: false),
                        Role = c.String(),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(),
                        oldjob = c.String(),
                        position = c.String(),
                        cv = c.String(),
                        qualificatin = c.String(),
                        time = c.String(),
                        orgaddress = c.String(),
                        interest = c.String(),
                        instit = c.String(),
                        EnrollmentDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        GenrateClass_GenrateClassID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GenrateClass", t => t.GenrateClass_GenrateClassID)
                .Index(t => t.GenrateClass_GenrateClassID);
            
            CreateTable(
                "dbo.AdminContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Titile = c.String(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        Credits = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        InstructorID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Person", t => t.InstructorID)
                .Index(t => t.InstructorID);
            
            CreateTable(
                "dbo.OfficeAssignment",
                c => new
                    {
                        InstructorID = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.InstructorID)
                .ForeignKey("dbo.Person", t => t.InstructorID)
                .Index(t => t.InstructorID);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        EnrollmentID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.EnrollStudent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GenrateClassID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GenrateClass", t => t.GenrateClassID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.GenrateClassID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.GenrateClass",
                c => new
                    {
                        GenrateClassID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Section = c.Int(),
                        CourseID = c.Int(nullable: false),
                        InstructorID = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GenrateClassID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: false)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: false)
                .ForeignKey("dbo.Person", t => t.InstructorID, cascadeDelete: false)
                .Index(t => t.CourseID)
                .Index(t => t.InstructorID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.PTMCalls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        Titile = c.String(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SuperAdminContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Titile = c.String(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourseInstructor",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        InstructorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.InstructorID })
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.InstructorID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.InstructorID);
            
            CreateStoredProcedure(
                "dbo.Department_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 50),
                        Budget = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                        StartDate = p.DateTime(),
                        InstructorID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Department]([Name], [Budget], [StartDate], [InstructorID])
                      VALUES (@Name, @Budget, @StartDate, @InstructorID)
                      
                      DECLARE @DepartmentID int
                      SELECT @DepartmentID = [DepartmentID]
                      FROM [dbo].[Department]
                      WHERE @@ROWCOUNT > 0 AND [DepartmentID] = scope_identity()
                      
                      SELECT t0.[DepartmentID], t0.[RowVersion]
                      FROM [dbo].[Department] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID"
            );
            
            CreateStoredProcedure(
                "dbo.Department_Update",
                p => new
                    {
                        DepartmentID = p.Int(),
                        Name = p.String(maxLength: 50),
                        Budget = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                        StartDate = p.DateTime(),
                        InstructorID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"UPDATE [dbo].[Department]
                      SET [Name] = @Name, [Budget] = @Budget, [StartDate] = @StartDate, [InstructorID] = @InstructorID
                      WHERE (([DepartmentID] = @DepartmentID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Department] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID"
            );
            
            CreateStoredProcedure(
                "dbo.Department_Delete",
                p => new
                    {
                        DepartmentID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"DELETE [dbo].[Department]
                      WHERE (([DepartmentID] = @DepartmentID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Department_Delete");
            DropStoredProcedure("dbo.Department_Update");
            DropStoredProcedure("dbo.Department_Insert");
            DropForeignKey("dbo.EnrollStudent", "StudentID", "dbo.Person");
            DropForeignKey("dbo.EnrollStudent", "GenrateClassID", "dbo.GenrateClass");
            DropForeignKey("dbo.Person", "GenrateClass_GenrateClassID", "dbo.GenrateClass");
            DropForeignKey("dbo.GenrateClass", "InstructorID", "dbo.Person");
            DropForeignKey("dbo.GenrateClass", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.GenrateClass", "CourseID", "dbo.Course");
            DropForeignKey("dbo.CourseInstructor", "InstructorID", "dbo.Person");
            DropForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Enrollment", "StudentID", "dbo.Person");
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "InstructorID", "dbo.Person");
            DropForeignKey("dbo.OfficeAssignment", "InstructorID", "dbo.Person");
            DropIndex("dbo.CourseInstructor", new[] { "InstructorID" });
            DropIndex("dbo.CourseInstructor", new[] { "CourseID" });
            DropIndex("dbo.GenrateClass", new[] { "DepartmentID" });
            DropIndex("dbo.GenrateClass", new[] { "InstructorID" });
            DropIndex("dbo.GenrateClass", new[] { "CourseID" });
            DropIndex("dbo.EnrollStudent", new[] { "StudentID" });
            DropIndex("dbo.EnrollStudent", new[] { "GenrateClassID" });
            DropIndex("dbo.Enrollment", new[] { "StudentID" });
            DropIndex("dbo.Enrollment", new[] { "CourseID" });
            DropIndex("dbo.OfficeAssignment", new[] { "InstructorID" });
            DropIndex("dbo.Department", new[] { "InstructorID" });
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            DropIndex("dbo.Person", new[] { "GenrateClass_GenrateClassID" });
            DropTable("dbo.CourseInstructor");
            DropTable("dbo.SuperAdminContact");
            DropTable("dbo.PTMCalls");
            DropTable("dbo.GenrateClass");
            DropTable("dbo.EnrollStudent");
            DropTable("dbo.Enrollment");
            DropTable("dbo.OfficeAssignment");
            DropTable("dbo.Department");
            DropTable("dbo.Course");
            DropTable("dbo.AdminContact");
            DropTable("dbo.Person");
        }
    }
}
