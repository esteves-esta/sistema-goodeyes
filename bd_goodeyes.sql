create database bd_goodeyes
default charset utf8 
default collate utf8_general_ci;

use bd_goodeyes;

-- drop database bd_goodeyes;

/* -- NOMENCLATURA
	no_   = numero
	nm_   = nome
    sg_   = sigla
    cd_   = codigo
    qt_   = quantidate
    vl_   = valor
    ds_   = descricao
*/

create table tbLogin(
email varchar(60) primary key,
senha varchar(8) not null,
nivel_acesso int not null -- Cliente (1), Funcionário (2), Gerente (3)
)default charset=utf8;


create table tbCliente(
cd_cliente int auto_increment primary key,
email varchar(60) references tbLogin (email),
nome varchar(30) not null,
sobrenome varchar(50) not null,
no_cpf varchar(14) not null,
no_tel varchar(14) , -- (OPCIONAL)
no_cel varchar(15) not null,
dt_nascimento varchar(10) not null,
nm_rua varchar(50) not null,
no_rua varchar(5) not null,
no_cep varchar(9) not null,
bairro varchar(50) not null,
cidade varchar(50) not null,
estado varchar(50) not null,
sg_uf varchar(2) not null,
complemento varchar(30) not null
)default charset=utf8;

create table tbReceita(
cd_receita int auto_increment primary key,
cd_cliente int references tbCliente (cd_cliente),
olho_direito varchar(40) not null,
olho_esquerdo varchar(40) not null, -- adição
distancia_pupilar varchar(10) not null,
nm_oftalmo varchar(50) not null,
sobrenome_oftalmo varchar(50) not null,
dt_receita varchar(10) not null,
dt_validade varchar(10) not null,
observacao varchar(255) not null
)default charset=utf8;


create table tbFuncionario(
cd_funcionario int auto_increment primary key,
email varchar(60) references tbLogin (email),
nome varchar(30) not null,
sobrenome varchar(50) not null,
no_cpf varchar(14) not null,
cargo varchar(30) not null,
no_tel varchar(14), -- (OPCIONAL)
no_cel varchar(15) not null,
dt_nascimento varchar(10) not null,
nm_rua varchar(50) not null,
no_rua varchar(5) not null,
no_cep varchar(9) not null,
bairro varchar(50) not null,
cidade varchar(50) not null,
estado varchar(50) not null,
sg_uf varchar(2) not null,
complemento varchar(30) not null
)default charset=utf8;



create table tbFornecedor(
cd_fornecedor int auto_increment  primary key,
nome varchar(30) not null,
sobrenome varchar(50) not null,
email varchar(50) not null,
no_tel varchar(14) not null,
no_cnpj varchar(18) not null
)default charset=utf8;

-- tbMarcaFornecida
create table tbFornecedorMarca(
nm_marca varchar(50) primary key,
cd_fornecedor int references tbFornecedor (cd_fornecedor)
)default charset=utf8;


create table tbProduto(
cd_produto int auto_increment primary key,
nm_marca varchar(50) references tbFornecedorMarca (nm_marca),
tipo varchar(60) not null,
descricao varchar(255) not null, 
aspecto varchar(255) not null,
vl_preco_unitario decimal(10,2) not null,
garantia varchar(20) not null,
caminho_imagem varchar(255) not null
)default charset=utf8;

create table tbEstoque(
cd_estoque int auto_increment primary key,
cd_produto int references tbProduto(cd_produto),
qt_estoque int not null
)default charset=utf8;


create table tbPedido(
cd_pedido int auto_increment primary key,
cd_cliente int references tbCliente(cd_cliente),
dt_pedido varchar(10) not null
)default charset=utf8;

create table tbPedidoItens(
cd_item int auto_increment primary key,
cd_pedido int references tbPedido (cd_pedido),
cd_produto int references tbProduto (cd_produto),
qt_item int not null,
vl_subtotal decimal(10,2) not null
)default charset=utf8;

create table tbPedidoReceita(
cd_pedidoreceita int auto_increment primary key,
cd_pedido int references tbPedido(cd_pedido),
cd_receita int references tbReceita(cd_receita)
)default charset=utf8;


create table tbFormaPagamento(
cd_pagamento int auto_increment primary key,
cd_pedido int references tbPedido(cd_pedido),
tipo_pagamento varchar(255) not null,
parcelamento varchar(40) not null, -- (informações do cartão ou numero do boleto)
vl_total decimal(10,2) not null
)default charset=utf8;


create table tbProdutoNotificacao(
cd_notificacao int auto_increment primary key,
cd_cliente int references tbCliente(cd_cliente),
cd_produto int references tbProduto(cd_produto)
)default charset=utf8;


create table tbCarrinhoCompra(
cd_carrinho int auto_increment primary key,
cd_produto int references tbProduto(cd_produto),
cd_cliente int references tbCliente(cd_cliente),
qt_item int
)default charset=utf8;

-- nova tabela
create table tbCarrinhoReceita(
cd_carrinhoreceita int auto_increment primary key,
cd_carrinho int references tbCarrinhoCompra (cd_carrinho),
cd_cliente int references tbCliente(cd_cliente),
cd_receita int references tbReceita(cd_receita)
)default charset=utf8;

