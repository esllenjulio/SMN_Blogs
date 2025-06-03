# SNM_Blogs
Projeto para treinamento Baseado em  um Blog

OBS1: Esta API foi criada para funcionar usando Entity Framework (EF) e Procedures Cadastradas no banco de dados SQL SERVER, 
      contodo irá existir metodos semelhando definidos pelo nome de cada recurso!
  
  Executando Api

- Definir api com projeto principal 
- Alterar a stringConnection com dados referente a base sql server. 
- Rodar as migrations 
  * Selecione o projeto de infrastructure e rode o comando update-database;
 
    OBS2: Após Executar as migrações para o banco de dados, execute também o script abaixo para criar as procedures no banco de dados.
```    
USE DB_BLOG;

CREATE OR ALTER PROCEDURE sp_GetAllPosts
AS
BEGIN
    SELECT Id, Title, Content, (select count(*) from Comment cc where cc.PostId = pp.Id) as CountComments FROM Post pp;
END;

/* Exemplo */
    EXECUTE sp_GetAllPosts;

CREATE OR ALTER PROCEDURE sp_CreatePost
    @Title NVARCHAR(200),
    @Content NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Post (Title, Content)
    VALUES (@Title, @Content);

    SELECT SCOPE_IDENTITY() AS Id;
END;

/* Exemplo */
EXECUTE sp_CreatePost @Title = TESTE2222, @Content = TESTE2222

CREATE OR ALTER PROCEDURE sp_GetPostWithComments
    @PostId INT
AS
BEGIN
    SELECT
        p.Id AS PostId,
        p.Title,
        p.Content,
        c.Id AS CommentId,
        c.Description AS CommentDescription
    FROM Post p
    LEFT JOIN Comment c ON c.PostId = p.Id
    WHERE p.Id = @PostId
END
/* Exemplo */
EXECUTE sp_GetPostWithComments @PostId = 3;

CREATE OR ALTER PROCEDURE sp_AddComment
    @PostId INT,
    @Description NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Comment (PostId, Description)
    VALUES (@PostId, @Description);
    SELECT SCOPE_IDENTITY() AS Id;
END;

/* Exemplo */
     EXECUTE sp_AddComment @PostId = 3, @Description = 'ola mundo';   
``` 

