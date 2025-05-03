## Aplicação de uma mini loja virtual e API RESTfull

## Apresentação

Este projeto é uma entrega do MBA DevXpert Full Stack .NET e refere-se ao módulo Introdução ao Desenvolvimento ASP.NET Core.

O principal objetivo é desenvolver uma plataforma web básica com uma interface intuitiva, que permita ao usuário realizar registro e login, bem como cadastrar, visualizar, atualizar e remover categorias e produtos. Além disso, os dados podem ser consultados e manipulados por meio de uma API REST simples.

A aplicação foi desenvolvida em duas versões separadas: uma em ASP.NET MVC e outra como API REST. Ambas oferecem as mesmas funcionalidades, porém a aplicação MVC não consome a API — elas funcionam de forma independente, mas compartilham a mesma estrutura e lógica de negócio.

## Autor(es)
- Leonardo Da Silva Rocha

## Proposta do Projeto

O projeto consiste em:

- **Aplicação MVC:** Interface web para uma Mini Loja Virtual.
- **API RESTful:** Exposição dos recursos da Mini Loja Virtual para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação básica de controle de acesso com registro e login de usuários.
- **Acesso a Dados:** Implementação do acesso ao banco de dados utilizando o ORM Entity Framework, com abordagem Code First.

## Tecnologias Utilizadas

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:** SQL Server
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estilização básica
- **Documentação da API:** Swagger

## Estrutura do Projeto

A estrutura do projeto é organizada da seguinte forma:

- src/
  - LinkBuyMvc/ - Projeto MVC
  - LinkBuyApi/ - API RESTful
  - LinkBuyLibrary/ - Biblioteca de Classes (contém o CRUD, banco de dados e DbContext, utilizada tanto pela API quanto pelo MVC)
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## Funcionalidades Implementadas

- **CRUD para categorias e produtos:** Permite criar, editar, visualizar e excluir categorias e produtos.
- **Autenticação e Autorização:** Implementação básica de controle de acesso com registro e login de usuários.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- SQL Server
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   
   - `git clone https://github.com/Leonardo-Da-Silva-Rocha/LinkBuy-Projeto-MBA.git`

2. **Configuração do Banco de Dados:**
   
   - No arquivo appsettings.json, você pode escolher qual banco de dados utilizar
   
   - SQLite (padrão) A string de conexão para SQLite já está configurada por padrão. O banco de dados será gerado automaticamente e populado com os dados iniciais através do Seed.
	
   - Caso prefira usar o SQL Server, altere a string de conexão.

3. **Executar a Aplicação MVC:**
   
   - No Visual Studio ou em sua ide de preferencia, selecione o projeto MVC como projeto de inicialização.

   - Execute a aplicação.

   - Acesse em: https://localhost:7250/

4. **Executar a API:**
   
   - No Visual Studio, selecione o projeto API como projeto de inicialização.
   
   - Execute a aplicação.

   - Acesse a documentação Swagger em: https://localhost:7047/swagger/index.html

## Instruções de Configuração

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## Documentação da API

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

https://localhost:7047/swagger/index.html