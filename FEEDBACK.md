# Feedback - Avaliação Geral

## Front End
### Navegação
  * Pontos positivos:
    - Possui views e rotas definidas no projeto LinkBuyMvc

### Design
    - Será avaliado na entrega final

### Funcionalidade
  * Pontos positivos:
    - CRUD de entidades implementados

## Back End
### Arquitetura
  * Pontos positivos:
    - Estrutura em três projetos principais:
      * LinkBuyMvc
      * LinkBuyLibrary
      * ProjetoMba

  * Pontos negativos:
    - Estrutura real diferente da documentada no README
    - Nome do projeto ProjetoMba não segue o padrão de nomenclatura dos demais, aparenta ser o projeto de API
    - Falta de clareza na separação de responsabilidades entre os projetos
    - Migrations no projeto MVC ao invés no projeto centralizador "Library"

### Funcionalidade
  * Pontos positivos:    
    - Configuração de Seed de dados mencionada

  * Pontos negativos:
    - Implementação do ASP.NET Identity não encontrada

### Modelagem
  * Pontos positivos:
    - Uso do Entity Framework Core
    - Biblioteca compartilhada (LinkBuyLibrary)

  * Pontos negativos:
    - Estrutura de projetos não reflete a arquitetura documentada no README

## Projeto
### Organização
  * Pontos positivos:
    - Arquivo solution (ProjetoMba.sln) na raiz
    - .gitignore e .gitattributes adequados
    - Separação em projetos distintos

  * Pontos negativos:
    - Falta da pasta src na raiz
    - Inconsistência entre a estrutura real e a documentada
    - Projetos diretamente na raiz do repositório

### Documentação
  * Pontos positivos:
    - README.md presente com:
      * Apresentação do projeto
      * Tecnologias utilizadas
      * Estrutura do projeto
      * Instruções de execução
    - Arquivo FEEDBACK.md presente
    - Documentação da API via Swagger mencionada

  * Pontos negativos:
    - Documentação desatualizada em relação à estrutura real do projeto
    - Inconsistência na descrição das funcionalidades (posts/comentários vs categorias/produtos)
    - URLs de exemplo no README não correspondem ao repositório atual

### Instalação
  * Pontos positivos:
    - Instruções básicas de instalação presentes
    - Configuração de Seed de dados mencionada

  * Pontos negativos:
    - Instruções de instalação incompletas
    - URLs de acesso incorretas no README
    - Seed de dados incompleto