-- Boa Prática: Nomenclatura Consistente.
-- Usar prefixos (T_ para tabelas) ou sufixos (_tb) ajuda na legibilidade.
-- Usar prefixos em colunas (id_, nm_, ds_, dt_, st_) indica o tipo de dado:
-- id_ (identificador), nm_ (nome), ds_ (descrição), dt_ (data/hora), st_ (status/booleano)

-- ---------------------------------
-- 1. Tabelas de Autenticação e Autorização (Usuário, Perfil, Permissão)
-- ---------------------------------

-- Tabela de Perfis/Cargos (Ex: Administrador, Técnico, Usuário Comum)
CREATE TABLE T_PERFIL (
    id_perfil SERIAL PRIMARY KEY, -- SERIAL ou INT IDENTITY(1,1) para auto-incremento
    nm_nome VARCHAR(100) NOT NULL UNIQUE
);

-- Tabela de Permissões (Ex: "CRIAR_CHAMADO", "DELETAR_USUARIO")
CREATE TABLE T_PERMISSAO (
    id_permissao SERIAL PRIMARY KEY,
    ds_chave VARCHAR(100) NOT NULL UNIQUE, -- Chave de código (boa prática para o software)
    ds_descricao VARCHAR(255) NOT NULL     -- Descrição amigável
);

-- Boa Prática: Relação Muitos-para-Muitos (N:N)
-- Um Perfil (N) pode ter várias Permissões (N).
-- Resolvemos isso com uma Tabela de Junção.
CREATE TABLE T_PERFIL_PERMISSAO (
    id_perfil INT NOT NULL,
    id_permissao INT NOT NULL,
    
    -- Chave primária composta garante que a dupla (perfil, permissao) seja única
    PRIMARY KEY (id_perfil, id_permissao), 
    
    FOREIGN KEY (id_perfil) REFERENCES T_PERFIL(id_perfil),
    FOREIGN KEY (id_permissao) REFERENCES T_PERMISSAO(id_permissao)
);

CREATE TABLE T_USUARIO (
    id_usuario SERIAL PRIMARY KEY,
    nm_nome VARCHAR(255) NOT NULL,
    ds_username VARCHAR(100) NOT NULL UNIQUE,
    
    -- Boa Prática de Segurança: NUNCA armazene senhas em texto puro.
    -- Armazene um HASH (ex: bcrypt, Argon2) da senha.
    ds_senha_hash VARCHAR(255) NOT NULL, 
    
    st_ativo BOOLEAN NOT NULL DEFAULT TRUE,
    
    -- Relação 1:N -> Um Perfil pode ter N Usuários.
    id_perfil INT NOT NULL,
    FOREIGN KEY (id_perfil) REFERENCES T_PERFIL(id_perfil)
);

-- ---------------------------------
-- 2. Tabelas de Domínio/Lookup (Catálogos)
-- ---------------------------------

-- Boa Prática: Tabelas de Domínio (Lookup Tables)
-- Evita "magic strings" (textos fixos) na tabela principal.
-- Se você precisar renomear "Aberto" para "Novo", você muda em 1 lugar aqui,
-- em vez de atualizar milhões de registros em T_CHAMADO.
-- Também melhora a performance de consulta (indexar INT é mais rápido que STRING).

CREATE TABLE T_SITUACAO (
    id_situacao SERIAL PRIMARY KEY,
    nm_situacao VARCHAR(100) NOT NULL UNIQUE -- Ex: Aberto, Em Andamento, Pendente, Fechado
);

CREATE TABLE T_PRIORIDADE (
    id_prioridade SERIAL PRIMARY KEY,
    nm_prioridade VARCHAR(100) NOT NULL UNIQUE, -- Ex: Baixa, Média, Alta, Urgente
    nr_nivel INT NOT NULL -- (Boa prática para ordenação, ex: 1=Urgente, 4=Baixa)
);


-- ---------------------------------
-- 3. Tabelas Principais (Core do Negócio)
-- ---------------------------------

CREATE TABLE T_CHAMADO (
    id_chamado SERIAL PRIMARY KEY,
    ds_titulo VARCHAR(255) NOT NULL,
    ds_descricao TEXT NOT NULL,
    dt_abertura TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    dt_fechamento TIMESTAMP NULL, -- Permite NULO, pois o chamado pode estar aberto
    
    -- Chaves Estrangeiras (Foreign Keys)
    
    -- Relação (Inferida do ChamadoService): Quem abriu o chamado?
    id_solicitante INT NOT NULL, 
    
    -- Relação: Quem é o técnico responsável? (Pode ser nulo se não atribuído)
    id_tecnico_responsavel INT NULL,
    
    -- Relação: Qual a situação? (1:N)
    id_situacao INT NOT NULL,
    
    -- Relação: Qual a prioridade? (1:N)
    id_prioridade INT NOT NULL,
    
    FOREIGN KEY (id_solicitante) REFERENCES T_USUARIO(id_usuario),
    FOREIGN KEY (id_tecnico_responsavel) REFERENCES T_USUARIO(id_usuario),
    FOREIGN KEY (id_situacao) REFERENCES T_SITUACAO(id_situacao),
    FOREIGN KEY (id_prioridade) REFERENCES T_PRIORIDADE(id_prioridade)
);

-- Tabela de Interações (Comentários, atualizações)
-- Relação 1:N -> Um Chamado (1) pode ter várias Interações (N)
CREATE TABLE T_INTERACAO (
    id_interacao SERIAL PRIMARY KEY,
    ds_texto TEXT NOT NULL,
    
    -- Boa Prática: Armazenamento de Arquivos
    -- Não armazene o arquivo (BLOB) direto no banco, isso o deixa lento e inchado.
    -- Armazene o CAMINHO ou URL para o arquivo (ex: S3, Azure Blob, ou pasta no servidor).
    ds_anexo_url VARCHAR(1024) NULL,
    
    dt_criacao TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    -- Relação: Quem fez esta interação?
    id_autor INT NOT NULL,
    
    -- Relação: A qual chamado esta interação pertence?
    id_chamado INT NOT NULL,
    
    FOREIGN KEY (id_autor) REFERENCES T_USUARIO(id_usuario),
    FOREIGN KEY (id_chamado) REFERENCES T_CHAMADO(id_chamado) ON DELETE CASCADE
    -- ON DELETE CASCADE: Se o chamado for deletado, suas interações vão junto.
);

-- ---------------------------------
-- 4. Tabelas Auxiliares (Base de Conhecimento)
-- ---------------------------------

CREATE TABLE T_BASE_CONHECIMENTO (
    id_base_conhecimento SERIAL PRIMARY KEY,
    ds_titulo VARCHAR(255) NOT NULL,
    ds_conteudo TEXT NOT NULL,
    dt_criacao TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    dt_atualizacao TIMESTAMP NULL,
    
    -- O diagrama pedia "atualizadoPor", mas é bom saber quem criou também.
    id_criado_por INT NOT NULL,
    id_atualizado_por INT NULL,
    
    FOREIGN KEY (id_criado_por) REFERENCES T_USUARIO(id_usuario),
    FOREIGN KEY (id_atualizado_por) REFERENCES T_USUARIO(id_usuario)
);

-- Boa Prática: Normalização (1ª Forma Normal - 1NF)
-- O diagrama mostra "palavrasChave: string[]" (um array).
-- Armazenar "tag1,tag2,tag3" em uma coluna é uma MÁ PRÁTICA.
-- Viola a 1ª Forma Normal (atomicidade) e torna impossível buscar "artigos com a tag2".
-- A solução correta é uma relação N:N.

CREATE TABLE T_PALAVRA_CHAVE (
    id_palavra_chave SERIAL PRIMARY KEY,
    ds_palavra VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE T_BASE_CONHECIMENTO_PALAVRA_CHAVE (
    id_base_conhecimento INT NOT NULL,
    id_palavra_chave INT NOT NULL,
    
    PRIMARY KEY (id_base_conhecimento, id_palavra_chave),
    FOREIGN KEY (id_base_conhecimento) REFERENCES T_BASE_CONHECIMENTO(id_base_conhecimento) ON DELETE CASCADE,
    FOREIGN KEY (id_palavra_chave) REFERENCES T_PALAVRA_CHAVE(id_palavra_chave) ON DELETE CASCADE

);