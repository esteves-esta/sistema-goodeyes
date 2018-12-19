/*
	PROCEDURES DO BANCO DE DADOS GOODEYES

	**ALTERAR************************************************************************
*/


/* -- LOGIN*/
DELIMITER $$
drop procedure if exists pa_alterarLogin $$ 
	create Procedure pa_alterarLogin (
	$email varchar (60),
	$senha varchar (8), 
	$nivel_acesso int
)
main: begin
	update tbLogin
	set senha =  $senha, nivel_acesso =  $nivel_acesso
	where email =  $email;
end$$
DELIMITER ;

call pa_alterarLogin ('leticia.vieira@outlook.com', '12345678', 2); 


/* -- CLIENTE*/

DELIMITER $$
drop procedure if exists pa_alterarCliente $$	-- se existir essa procedure ele vai excluir 
create Procedure pa_alterarCliente (
	$cd_cliente int,
	$email varchar (50), 
	$nome varchar (30), 
	$sobrenome varchar(50), 
	$no_cpf varchar (14),
	$no_tel varchar (14), 
	$no_cel varchar (15),
	$dt_nascimento varchar (10), 
	$nm_rua varchar (50), 
	$no_rua varchar (5), 
	$no_cep varchar (9), 
	$bairro varchar (50), 
	$cidade varchar (50),
	$estado varchar (50),
	$sg_uf varchar (2),
	$complemento varchar (30)
)
main: begin
	update tbCliente
	set 
    nome = $nome, 
    sobrenome = $sobrenome, 
    no_cpf = $no_cpf, 
    no_tel = $no_tel, 
    no_cel = $no_cel,  
    dt_nascimento = $dt_nascimento,  
	nm_rua = $nm_rua, 
    no_rua = $no_rua, 
    no_cep = $no_cep, 
    bairro = $bairro, 
    cidade = $cidade, 
    estado = $estado, 
    sg_uf = $sg_uf, 
    complemento = $complemento
	where cd_cliente = $cd_cliente;
end$$
DELIMITER ;

-- call pa_alterarCliente(1, 'ana.silva@outlook.com','Ana', 'Silva', '522.171.988-02', '(11)3947-5213', '(11)96554-2536', '02/06/1990', 'Rua 2', '45', '02990-270', 'Jardim Amélia','Pirapora', 'São Paulo', 'SP', 'apt A');

/* -- RECEITA*/

DELIMITER $$
drop procedure if exists pa_alterarReceita $$
create Procedure pa_alterarReceita (
	$cd_receita int,
	$cd_cliente int,
	$olho_direito varchar (40),
	$olho_esquerdo varchar (40),
	$distancia_pupilar varchar (10),
	$nm_oftalmo varchar (50),
	$sobrenome_oftalmo varchar(50),
	$dt_receita varchar(10),
	$dt_validade varchar(10),
	$observacao varchar (255)
)
main: begin
	update tbReceita
	set cd_cliente = $cd_cliente, 
	olho_direito = $olho_direito, 
	olho_esquerdo = $olho_esquerdo, 
	distancia_pupilar = $distancia_pupilar, 
	nm_oftalmo = $nm_oftalmo, 
	sobrenome_oftalmo = $sobrenome_oftalmo, 
	dt_receita = $dt_receita, 
	dt_validade = $dt_validade, 
    observacao= $observacao
	where cd_receita = $cd_receita;
end $$
DELIMITER ;

call pa_alterarReceita (1,1,'+1,25 -1,00 180° +1', '+1,50 -1,00 180° +1', '10mm','Luzia', 'Matos', '15/04/2018', '15/05/2018', 'Lente transitions');



/* -- FUNCIONÁRIO*/
DELIMITER $$
drop procedure if exists pa_alterarFuncionario $$
create Procedure pa_alterarFuncionario (
	$cd_funcionario int,
	$email varchar(60),
	$nome varchar(30),
	$sobrenome varchar(50),
	$no_cpf varchar(14),
	$cargo varchar(30),
	$no_tel varchar(14),
	$no_cel varchar(15),
	$dt_nascimento varchar(10),
	$nm_rua varchar(50),
	$no_rua varchar(5),
	$no_cep varchar(9),
	$bairro varchar(50),
	$cidade varchar(50),
	$estado varchar(50),
	$sg_uf varchar(2),
	$complemento varchar(30)
)
main: begin
	update tbFuncionario
	set  
    nome = $nome, 
    sobrenome = $sobrenome,
    no_cpf = $no_cpf, 
    cargo = $cargo,
    no_tel = $no_tel,
    no_cel = $no_cel,
    dt_nascimento = $dt_nascimento,
	nm_rua  = $nm_rua, 
    no_rua  = $no_rua , 
    no_cep = $no_cep, 
    bairro = $bairro, 
    cidade = $cidade, 
    estado = $estado,
    sg_uf = $sg_uf, 
	complemento = $complemento
	where cd_funcionario = $cd_funcionario;
end$$
DELIMITER ;

-- call pa_alterarFuncionario (2, 'Jorge', 'Santos Barbosa', '254.569.744-22',  'Atendente', '(11) 1111-4521', '(11) 96532-1111', '13/06/1990', 'Rua 1', '02', '11555-222', 'Av. Ramos', 'Araraquara','São Paulo', 'SP', 'nenhum');



/* -- FORNECEDOR*/
DELIMITER $$
drop procedure if exists pa_alterarFornecedor $$
create Procedure pa_alterarFornecedor (
	$cd_fornecedor int,
	$nome varchar(30),
	$sobrenome varchar(50),
	$email varchar(50),
	$no_tel varchar(14),
	$no_cnpj varchar(18)
)
main: begin
	update tbFornecedor
	set  nome=$nome, sobrenome=$sobrenome, email=$email, no_tel=$no_tel, no_cnpj=$no_cnpj
	where cd_fornecedor = $cd_fornecedor;
end $$
DELIMITER ;

call pa_alterarFornecedor (1, 'Rafael', 'Santos Costa', 'rafael.santos@outlook.com', '(11)5555-2010', '11.245.578/9991-30');






 /* -- FORNECEDOR MARCA*/
DELIMITER $$
drop procedure if exists pa_alterarFornecedorMarca $$
create Procedure pa_alterarFornecedorMarca (
	$nm_marca varchar (50), $cd_fornecedor int
)
main: begin
	update tbFornecedorMarca
	set cd_fornecedor =  $cd_fornecedor
	where nm_marca =  $nm_marca;
end $$
DELIMITER ;

call pa_alterarFornecedorMarca ('Adidas',3);






/* -- PRODUTO*/ 
DELIMITER $$
drop procedure if exists pa_alterarProduto $$
create Procedure pa_alterarProduto (
	$cd_produto int,
	$nm_marca varchar(50),
	$tipo varchar(60),
	$descricao varchar(255), 
	$aspecto varchar(255),
	$vl_preco_unitario decimal(10,2),
	$garantia varchar(20),
	$caminho_imagem varchar(255)
)
main: begin
	update tbProduto
	set 
    nm_marca = $nm_marca, 
    tipo = $tipo, 
    descricao = $descricao, 
    aspecto = $aspecto, 
    vl_preco_unitario = $vl_preco_unitario,
    garantia = $garantia, 
    caminho_imagem = $caminho_imagem
	where cd_produto = $cd_produto;
end$$
DELIMITER ;




/* -- ESTOQUE*/ 
DELIMITER $$
drop procedure if exists pa_alterarEstoque $$

create Procedure pa_alterarEstoque (
	$cd_produto int, $qt_estoque int
)
main: begin
	update tbEstoque
	set qt_estoque = $qt_estoque
	where cd_produto = $cd_produto;
end$$
DELIMITER ;



/* -- PEDIDO*/ 
DELIMITER $$
drop procedure if exists pa_alterarPedido $$
create Procedure pa_alterarPedido (
	cd_pedido int,
	cd_cliente int,
	dt_pedido varchar(10)
)
main: begin
	update tbPedido
	set cd_cliente = $cd_cliente, 
    dt_pedido = $dt_pedido
	where cd_pedido = cd_pedido;
end$$
DELIMITER ;

-- call pa_alterarPedido (1,1,'28/08/2017');




/* -- PEDIDO ITENS*/ 
DELIMITER $$
drop procedure if exists pa_alterarPedidoItens $$
create Procedure pa_alterarPedidoItens(
	$cd_item int,
	$cd_pedido int,
	$cd_produto int,
	$qt_item int
)
main: begin
    update tbPedidoItens
    set 
    cd_pedido = $cd_pedido, 
    cd_produto = $cd_produto, 
    qt_item = $qt_item, 
    subtotal=(select vl_preco_unitario * $qt_item from tbProduto where cd_produto = $cd_produto)
    where cd_item = cd_item;
end$$
DELIMITER ;







/* -- PEDIDO RECEITA*/ 
DELIMITER $$
drop procedure if exists pa_alterarPedidoReceita $$
create Procedure pa_alterarPedidoReceita (
	$cd_pedido int, $cd_receita int, $cd_pedidoreceita int
)
main: begin
	update tbPedidoReceita
	set cd_pedido = $cd_pedido, cd_receita = $cd_receita
	where cd_pedidoreceita = $cd_pedidoreceita;
end$$
DELIMITER ;

call pa_alterarPedidoReceita (1,3, 3);








/* -- PAGAMENTO*/ 
DELIMITER $$
drop procedure if exists pa_alterarFormaPagamento $$
create Procedure pa_alterarFormaPagamento(
	$cd_pagamento int,
	$cd_pedido int, 
	$tipo_pagamento varchar(255), 
	$parcelamento varchar(40)
)
main: begin
    update tbFormaPagamento
    set cd_pedido = $cd_pedido, 
    tipo_pagamento = $tipo_pagamento, 
    parcelamento = $parcelamento, 
    vl_total=(select SUM(vl_subtotal)from tbPedidoItens where cd_pedido = $cd_pedido)
    where cd_pagamento = $cd_pagamento;
end$$
DELIMITER ;



/* -- NOTIFICACAO*/ 
DELIMITER $$
drop procedure if exists pa_alterarNotificacao $$
create Procedure pa_alterarNotificacao(
	$cd_notificacao int,
	$cd_cliente int,
	$cd_produto int
)
main: begin
	update tbProdutoNotificacao
	set cd_cliente = $cd_cliente, 
    cd_produto = $cd_produto
	where cd_notificacao = $cd_notificacao;
end$$
DELIMITER ;

call pa_alterarNotificacao(2,1,1);









/* -- CARRINHO*/ 
DELIMITER $$
drop procedure if exists pa_alterarCarrinhoCompra $$
create Procedure pa_alterarCarrinhoCompra(
	$cd_carrinho int,
    $cd_produto int,
    $cd_cliente int,
    $qt_item int
)
main: begin
	update tbCarrinhoCompra
	set cd_produto = $cd_produto, cd_cliente = $cd_cliente, qt_item = $qt_item
	where cd_carrinho = $cd_carrinho;
end$$
DELIMITER ;

-- call pa_alterarCarrinhoCompra(1,1,2,30);
