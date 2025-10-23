# PokeCore  TUI  Pokedex

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![Language](https://img.shields.io/badge/language-C%23-green.svg)
![Framework](https://img.shields.io/badge/.NET-Windows%20Forms-purple.svg)
![Status](https://img.shields.io/badge/status-Em%20Desenvolvimento-yellow.svg)

## 📜 Descrição

**PokeCore** é uma aplicação Desktop desenvolvida em C# (.NET) utilizando Windows Forms, projetada para gerenciar informações de Treinadores Pokémon e seus respectivos Pokémon capturados. O projeto segue uma arquitetura de 4 camadas (DTO, DAL, BLL, UI) para garantir organização, manutenibilidade e separação de responsabilidades. A persistência dos dados é realizada através de arquivos JSON.

Este projeto foi desenvolvido como parte de uma atividade acadêmica com o objetivo de aplicar os conceitos de arquitetura em camadas em uma aplicação desktop, utilizando as entidades `Admin`,`Treinador` e `Pokemon`.

## ✨ Funcionalidades Principais

### Para Treinadores (Usuários Comuns):

* **Autenticação:** Login seguro com usuário/email e senha (com hashing).
* **Cadastro:** Registro de novos treinadores com validação de dados e senha.
* **Dashboard (Home):** Visualização rápida de informações do treinador (último Pokémon capturado, contagem total).
* **PC Box:** Visualização da coleção completa de Pokémon capturados.
* **Edição de Time:** Gerenciamento do time ativo (até 6 Pokémon), movendo Pokémon entre o time e o PC Box.
* **Edição de Pokémon:** Alteração de apelido e local de captura dos Pokémon pertencentes ao treinador.
* **Edição de Perfil:** Atualização dos dados cadastrais (nome de usuário, nome de exibição, email) e foto de perfil.
* **Alteração de Senha:** Mudança da senha atual por uma nova, com validação da senha antiga.

### Para Administradores:

* **Gerenciamento de Treinadores:**
    * Listagem de todos os treinadores cadastrados.
    * Visualização e Edição dos dados de qualquer treinador (incluindo status de Admin e foto).
    * Criação de novos treinadores (com definição de status de Admin).
    * Exclusão de treinadores (com validação para não excluir admins ou treinadores com Pokémon).
* **Gerenciamento de Pokémon (Geral):**
    * Listagem de todos os Pokémon no sistema.
    * Criação de novos Pokémon.
    * Edição dos dados de qualquer Pokémon.
    * Atribuição/Desatribuição de Pokémon a treinadores.
    * Exclusão definitiva de Pokémon.
* **Gerenciamento Específico de Treinador:**
    * Visualização detalhada do perfil de um treinador selecionado.
    * Visualização do Time Ativo e PC Box do treinador selecionado.
    * **Liberação de Pokémon:** Permite ao admin liberar Pokémon pertencentes a um treinador específico (necessário antes de excluir um treinador com Pokémon).
* **Recuperação de Senha:** Reset de senha de usuários (requer autenticação do admin).

## 💻 Tecnologias Utilizadas

* **Linguagem:** C#
* **Framework:** .NET 8.0
* **Interface Gráfica:** Windows Forms
* **Biblioteca UI:** Guna UI2 Framework
* **Persistência:** Arquivos JSON
* **Hashing de Senha:** BCrypt.Net

## 🏗️ Arquitetura

O projeto está organizado em 4 camadas distintas:

1.  **PokeCore.DTO (Data Transfer Objects):** Define as classes simples (`PokemonDTO`, `TreinadorDTO`) que carregam os dados entre as camadas.
2.  **PokeCore.DAL (Data Access Layer):** Responsável pela interação com a fonte de dados (neste caso, leitura e escrita nos arquivos JSON). Contém os Repositórios (`PokemonRepository`, `TreinadorRepository`) e o `JsonPersistenceHelper`.
3.  **PokeCore.BLL (Business Logic Layer):** Contém as regras de negócio, validações e orquestração das operações. Interage com a DAL para buscar/salvar dados e fornece serviços para a UI (`PokemonServiceBLL`, `TreinadorServiceBLL`).
4.  **PokeCore (User Interface Layer):** A aplicação Desktop Windows Forms visível para o usuário. Contém os formulários (`frmLogin`, `frmMain`, etc.) e User Controls (`ucHome`, `ucPcBox`, etc.) que interagem com o usuário e consomem os serviços da BLL.

## ⚙️ Configuração e Instalação

**Pré-requisitos:**

* [.NET SDK](https://dotnet.microsoft.com/download) (versão compatível com o projeto, ex: 6.0 ou superior)
* [Visual Studio](https://visualstudio.microsoft.com/) (recomendado) com a carga de trabalho ".NET Desktop Development" instalada.

**Passos:**

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/Milonesco/PokeCore.git](https://github.com/Milonesco/PokeCore.git)
    cd PokeCore
    ```
2.  **Abra a solução:** Abra o arquivo `PokeCore.sln` no Visual Studio.
3.  **Restaure as dependências:** O Visual Studio deve fazer isso automaticamente. Se necessário, clique com o botão direito na Solução no "Solution Explorer" e escolha "Restore NuGet Packages".
4.  **Compile a solução:** Pressione `Ctrl+Shift+B` ou vá em `Build` > `Build Solution`.
5.  **Execute o projeto:** Pressione `F5` ou clique no botão "Start" (geralmente com o nome do projeto UI, `PokeCore.DesktopUI`, selecionado).

## 🚀 Uso

1.  Execute a aplicação.
2.  Na tela de Login (`frmLogin`), insira seu nome de usuário/email e senha.
    * Se for o primeiro acesso, clique em "Cadastre-se" para criar uma conta (`frmCadastro`).
    * *Nota:* Para acessar as funcionalidades de Admin, um usuário precisa ter a flag `IsAdmin` como `true` no arquivo `trainers.json` correspondente.
3.  Após o login, a tela principal (`frmMain`) será exibida.
4.  Utilize o menu lateral para navegar entre as diferentes seções: Home, PC Box, Editar Time, Gerenciar Treinadores (Admin), Gerenciar Pokémon (Admin), Configurações (Editar Perfil).

## 💾 Persistência de Dados

Os dados dos Treinadores e Pokémon são armazenados em arquivos JSON localizados na pasta `bin\Debug\netX.X-windows\data` (ou similar, dependendo da configuração de build):

* `trainers.json`: Armazena a lista de objetos `TreinadorDTO`.
* `pokemon.json`: Armazena a lista de objetos `PokemonDTO`.

A classe `JsonPersistenceHelper` na camada DAL é responsável por ler e escrever nesses arquivos.

## 📄 Licença

Este projeto está licenciado sob a Licença MIT. Veja o arquivo [LICENSE.txt](LICENSE.txt) para mais detalhes.

## 👥 Autores

* **[Gabriel Milone Vieira]** - [[Github](https://github.com/Milonesco)]
* **[Raphaga Willian Alexandre Lopes Ferreira]** - [[GitHub](https://github.com/raphinhagameplaymaroto)]