IdentifierTypes
	Insert_IdentifierType
		@IdentifierName 	NVARCHAR(100)
	Update_IdentifierType
		@IdentifierTypeID 	INT,
		@IdentifierName   	NVARCHAR(100)
	Delete_IdentifierType
		@IdentifierTypeID	INT
	Get_All_IdentifierTypes
	Get_IdentifierType_ByID
		@IdentifierTypeID 	INT
	

Identifiers
	Insert_Identifier
		@IdentifierTypeID	INT
		@IdentifierValue	NVARCHAR(100)
	Get_All_Identifiers
	Get_IdentifierByID
		@IdentifierID		INT
	Update_Identifier
		@IdentifierID		INT
		@IdentifierTypeID	INT
		@IdentifierValue	NVARCHAR(100)
	Delete_Identifier
		@IdentifierID		INT

Persons
	sp_Person_Insert
		@FirstName NVARCHAR(100),
    	@SecondName NVARCHAR(100),
        @ThirdName NVARCHAR(100),
        @LastName NVARCHAR(100),
        @IdentifierID INT,
        @Gender NVARCHAR(10),
        @BirthDate DATE,
        @Phone NVARCHAR(20),
        @Email NVARCHAR(100),
        @Address NVARCHAR(200),
        @IsActive BIT
	sp_Person_GetByID
		@PersonID INT
	sp_Person_Update
		@PersonID INT,
		@FirstName NVARCHAR(100),
		@SecondName NVARCHAR(100),
		@ThirdName NVARCHAR(100),
		@LastName NVARCHAR(100),
		@IdentifierID INT,
		@Gender NVARCHAR(10),
		@BirthDate DATE,
		@Phone NVARCHAR(20),
		@Email NVARCHAR(100),
		@Address NVARCHAR(200),
		@IsActive BIT
	sp_Person_Delete
		@PersonID INT
	sp_Person_GetAll
	sp_Person_GetActive
	sp_Person_UpdateIsActive
		@PersonID INT
        @IsActive BIT



    




EmployeeJobTitles
	Insert_EmployeeJobTitle
		@JobTitleName NVARCHAR(100),
		@Description NVARCHAR(255) = NULL,
		@IsTeaching BIT,
		@IsAdministrative BIT,
		@CanTeach BIT,
		@RequiresCertification BIT	
	Update_EmployeeJobTitle
		@JobTitleID INT,
    	@JobTitleName NVARCHAR(100),
    	@Description NVARCHAR(255) = NULL,
    	@IsTeaching BIT,
    	@IsAdministrative BIT,
    	@CanTeach BIT,
    	@RequiresCertification BIT
	Delete_EmployeeJobTitle
		@JobTitleID INT
	Get_All_EmployeeJobTitles
	Get_EmployeeJobTitle_ByID
	Get_Teaching_JobTitles
	Get_JobTitles_CanTeach
	Get_Administrative_JobTitles
	Get_JobTitles_RequireCertification
	Update_JobTitle_IsTeaching
		@JobTitleID INT,
    	@IsTeaching BIT
		







