## Aplicação de uma mini loja virtual e API RESTful

## Apresentação

Este projeto é uma entrega do MBA DevXpert Full Stack .NET e refere-se ao módulo Introdução ao Desenvolvimento ASP.NET Core.

O principal objetivo é desenvolver uma plataforma web básica com uma interface intuitiva, que permita ao usuário realizar registro e login, bem como cadastrar, visualizar, atualizar e remover categorias e produtos. Além disso, os dados podem ser consultados e manipulados por meio de uma API REST simples.

A aplicação foi desenvolvida em duas versões separadas: uma em ASP.NET MVC e outra como API REST. Ambas oferecem as mesmas funcionalidades, porém a aplicação MVC não consome a API — elas funcionam de forma independente, mas compartilham a mesma estrutura e lógica de negócio.

Autor(es)
- Leonardo Da Silva Rocha

Proposta do Projeto

O projeto consiste em:

- **Aplicação MVC:** Interface web para uma Mini Loja Virtual.
- **API RESTful:** Exposição dos recursos da Mini Loja Virtual para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação básica de controle de acesso com registro e login de usuários.
- **Acesso a Dados:** Implementação do acesso ao banco de dados utilizando o ORM Entity Framework, com abordagem Code First.

Tecnologias Utilizadas

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

Estrutura do Projeto

A estrutura do projeto é organizada da seguinte forma:


- src/
  - LinkBuyMvc.Web/ - Projeto MVC
  - LinkBuyApi.Api/ - API RESTful
  - LinkBuy.Data/ - Modelos de Dados e Configuração do EF Core
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **CRUD para Posts e Comentários:** Permite criar, editar, visualizar e excluir categorias e produtos.
- **Autenticação e Autorização:** Diferenciação entre usuários comuns e administradores.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- SQL Server
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/seu-usuario/nome-do-repositorio.git`
   - `cd nome-do-repositorio`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos

3. **Executar a Aplicação MVC:**
   - Acesse a aplicação em: http://localhost:7250

4. **Executar a API:**
   - Acesse a documentação da API em: http://localhost:5001/swagger

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:5001/swagger


