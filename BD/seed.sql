-- ---------------------------------
-- 02-Seed.sql (CargaInicial.sql)
-- ---------------------------------
-- Boa Prática: Este script é DML (Data Manipulation Language).
-- Ele insere os dados INICIAIS e ESSENCIAIS para a aplicação funcionar.
-- Rode este script APÓS o 01-Schema.sql.

-- Boa Prática: Usar uma transação (BEGIN/COMMIT) garante que
-- ou TUDO é inserido com sucesso, ou NADA é (em caso de erro).
BEGIN;

-- ---------------------------------
-- 1. Tabelas de Domínio/Lookup (Catálogos)
-- Estes são os valores que aparecerão em <select> (dropdowns) na aplicação.
-- ---------------------------------

INSERT INTO T_SITUACAO (nm_situacao) VALUES
('Aberto'),
('Em Andamento'),
('Pendente Cliente'),
('Resolvido'),
('Fechado');

INSERT INTO T_PRIORIDADE (nm_prioridade, nr_nivel) VALUES
('Baixa', 4),
('Média', 3),
('Alta', 2),
('Urgente', 1);

-- ---------------------------------
-- 2. Tabelas de Autenticação e Autorização
-- ---------------------------------

-- Primeiro, criamos os Perfis (Papéis)
INSERT INTO T_PERFIL (nm_nome) VALUES
('Administrador'),
('Técnico'),
('Usuário Comum');

-- Segundo, criamos as Permissões (Ações)
-- Baseado na lógica do seu diagrama (ChamadoService, Sistema)
INSERT INTO T_PERMISSAO (ds_chave, ds_descricao) VALUES
-- Usuário
('USUARIO_LISTAR', 'Listar todos os usuários do sistema'),
('USUARIO_CRIAR', 'Criar um novo usuário'),
('USUARIO_EDITAR_PERFIL', 'Editar o perfil de um usuário'),
-- Chamado
('CHAMADO_CRIAR', 'Abrir um novo chamado'),
('CHAMADO_VER_PROPRIOS', 'Ver apenas os próprios chamados'),
('CHAMADO_VER_TODOS', 'Ver todos os chamados do sistema'),
('CHAMADO_ATRIBUIR', 'Atribuir um chamado a um técnico'),
('CHAMADO_RESOLVER', 'Resolver/Fechar um chamado'),
-- Base de Conhecimento
('KB_CRIAR', 'Criar artigo na Base de Conhecimento'),
('KB_EDITAR', 'Editar artigo na Base de Conhecimento'),
('KB_LER', 'Ler a Base de Conhecimento');


-- ---------------------------------
-- 3. Relação Perfil <-> Permissão (Definindo as regras)
-- Boa Prática: Usamos subconsultas para pegar os IDs pelo nome.
-- Isso torna o script independente dos IDs auto-incrementados.
-- ---------------------------------

-- Administrador (pode tudo)
-- (Este é um truque: seleciona o ID do admin e faz um CROSS JOIN com todas as permissões)
INSERT INTO T_PERFIL_PERMISSAO (id_perfil, id_permissao)
SELECT 
    (SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Administrador'),
    id_permissao 
FROM 
    T_PERMISSAO;

-- Técnico (Pode ver/atribuir/resolver chamados e gerenciar KB)
INSERT INTO T_PERFIL_PERMISSAO (id_perfil, id_permissao) VALUES
((SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Técnico'), (SELECT id_permissao FROM T_PERMISSAO WHERE ds_chave = 'CHAMADO_VER_TODOS')),
((SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Técnico'), (SELECT id_permissao FROM T_PERMISSAO WHERE ds_chave = 'CHAMADO_ATRIBUIR')),
((SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Técnico'), (SELECT id_permissao FROM T_PERMISSAO WHERE ds_chave = 'CHAMADO_RESOLVER')),
((SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Técnico'), (SELECT id_permissao FROM T_PERMISSAO WHERE ds_chave = 'KB_CRIAR')),
((SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Técnico'), (SELECT id_permissao FROM T_PERMISSAO WHERE ds_chave = 'KB_EDITAR')),
((SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Técnico'), (SELECT id_permissao FROM T_PERMISSAO WHERE ds_chave = 'KB_LER'));

-- Usuário Comum (Pode criar/ver seus chamados e ler a KB)
INSERT INTO T_PERFIL_PERMISSAO (id_perfil, id_permissao) VALUES
((SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Usuário Comum'), (SELECT id_permissao FROM T_PERMISSAO WHERE ds_chave = 'CHAMADO_CRIAR')),
((SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Usuário Comum'), (SELECT id_permissao FROM T_PERMISSAO WHERE ds_chave = 'CHAMADO_VER_PROPRIOS')),
((SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Usuário Comum'), (SELECT id_permissao FROM T_PERMISSAO WHERE ds_chave = 'KB_LER'));


-- ---------------------------------
-- 4. Dados de Teste (Usuários Iniciais)
-- Para que você possa logar e testar os 3 perfis.
-- ---------------------------------

-- !! BOA PRÁTICA DE SEGURANÇA !!
-- Estes hashes SÃO EXEMPLOS. NUNCA use senhas em texto puro ("admin123").
-- Na sua aplicação .NET, use uma biblioteca como 'BCrypt.Net' para gerar o hash.
-- O hash abaixo é um exemplo de BCrypt para a senha 'admin123' (para todos os usuários).
INSERT INTO T_USUARIO (nm_nome, ds_username, ds_senha_hash, st_ativo, id_perfil) VALUES
('Administrador do Sistema', 'admin', '$2a$12$4L.A4TjA.D.cW9L/fJ6v.ONj0uI.f1N.p0yYg3C./Yv39l6p0U5rK', TRUE, (SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Administrador')),
('Técnico Suporte N1', 'tecnico', '$2a$12$4L.A4TjA.D.cW9L/fJ6v.ONj0uI.f1N.p0yYg3C./Yv39l6p0U5rK', TRUE, (SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Técnico')),
('Usuário de Teste', 'usuario', '$2a$12$4L.A4TjA.D.cW9L/fJ6v.ONj0uI.f1N.p0yYg3C./Yv39l6p0U5rK', TRUE, (SELECT id_perfil FROM T_PERFIL WHERE nm_nome = 'Usuário Comum'));

-- Finaliza a transação
COMMIT;