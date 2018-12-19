/*
	PROCEDURES DO BANCO DE DADOS GOODEYES

	****PESQUISA***************************************************************

*/


/* -- LOGIN PESQUISA POR EMAIL E SENHA*/
DELIMITER $$
drop procedure if exists pa_autenticaUsu $$
create Procedure pa_autenticaUsu(
	$email varchar(50),
	$senha varchar (8)
)
main: begin
	select count(*) from tbLogin where email = $email and senha = $senha;
end$$
DELIMITER ;


/* -- LOGIN PESQUISA POR NIVEL*/
DELIMITER $$
drop procedure if exists pa_verificaNivel $$
create Procedure pa_verificaNivel(
	$email varchar(50)
)
main: begin
	select * from tbLogin where email=$email;
end$$
DELIMITER ;


/* -- VERIFICA SE EMAIL EXISTE*/
DELIMITER $$
drop procedure if exists pa_verificaEmail $$
create Procedure pa_verificaEmail($email varchar(50))
main: begin
	select count(*) from tbLogin where email=$email;
end$$
DELIMITER ;



/* -- CLIENTE*/
DELIMITER $$
drop procedure if exists pa_pesquisarCliente $$
create Procedure pa_pesquisarCliente(
	$cd_cliente int, 
    $nome varchar(30), 
    $sobrenome varchar(30),
    $no_cpf varchar (14)
)
main: begin
	select * from tbCliente 
    where nome like concat("%", $nome, "%") 
    or sobrenome = concat("%", $sobrenome , "%")
    or no_cpf = $no_cpf 
    or cd_cliente = $cd_cliente;
end $$
DELIMITER ;






/* -- RECEITA*/
DELIMITER $$
drop procedure if exists pa_pesquisarReceita $$
create Procedure pa_pesquisarReceita($cd_cliente int)
main: begin
	select * from tbReceita where cd_cliente = $cd_cliente;
end $$
DELIMITER ;








/* -- FUNCIONÁRIO*/
DELIMITER $$
drop procedure if exists pa_pesquisarFuncionario $$
create Procedure pa_pesquisarFuncionario(
	$nome varchar(30),
    $sobrenome varchar(30),
    $no_cpf varchar(14), 
    $cargo varchar(30) 
)
main: begin
	select * from tbFuncionario
	where  cargo =  $cargo 
    or  nome =  $nome 
    or  sobrenome =  $sobrenome 
    or  no_cpf =  $no_cpf;
end $$
DELIMITER ;

call pa_pesquisarFuncionario ('','', '', 'Atendente');





/* -- FUNCIONÁRIO*/
DELIMITER $$
drop procedure if exists pa_pesquisarFuncionarioCod $$
create Procedure pa_pesquisarFuncionarioCod($cd_funcionario int)
main: begin
	select * from tbFuncionario
	where cd_funcionario = $cd_funcionario;
end $$
DELIMITER ;

-- call pa_pesquisarFuncionarioCod(3)









 /* -- FORNECEDOR*/
-- PRA FUNCIONAR PRECISA QUE A TABELA FORNECEDOR E FORNECEDOR MARCA ESTEJAM PREENCHIDAS
DELIMITER $$
drop procedure if exists pa_pesquisarFornecedor $$
create Procedure pa_pesquisarFornecedor(
	$no_cnpj varchar (18), 
	$nm_marca varchar (50)
)
main: begin
	select tbFornecedor.cd_fornecedor as 'Código Fornecedor', 
    nome as Nome, 
    sobrenome as Sobrenome, 
    email as 'E-mail', 
    no_tel as Telefone, 
    no_cnpj as CNPJ, 
    tbFornecedorMarca.nm_marca as 'Nome da marca' 
    from tbFornecedor
	inner join tbFornecedorMarca 
    on tbFornecedorMarca.cd_fornecedor = tbFornecedor.cd_fornecedor
	where no_cnpj = $no_cnpj 
    or nm_marca like concat("%", $nm_marca, "%");
end $$
DELIMITER ;

-- call pa_pesquisarFornecedor ('11.245.578/0001-30','');

-- -------------------




 /* -- FORNECEDOR MARCA*/
DELIMITER $$
drop procedure if exists pa_pesqFornecedorMarca $$
create Procedure pa_pesqFornecedorMarca(
$nm_marca varchar (50)
)
main: begin
	select nm_marca as 'Nome da marca', 
    tbfornecedor.cd_fornecedor as 'Código Fornecedor', 
    nome as 'Nome do Fornecedor',
    sobrenome as 'Sobrenome do Fornecedor'
    from tbFornecedorMarca
    inner join tbfornecedor
    on tbfornecedor.cd_fornecedor = tbfornecedormarca.cd_fornecedor
    where nm_marca like concat("%", $nm_marca, "%");
end $$
DELIMITER ;
 
-- call pa_exibirFornecedorMarca;





DELIMITER $$
drop procedure if exists pa_pesquisarMarca $$
create Procedure pa_pesquisarMarca(
	$nm_marca varchar (50)
)
main: begin
	select * from tbFornecedorMarca
	where nm_marca like concat("%", $nm_marca, "%");
end $$
DELIMITER ;





-- PRA FUNCIONAR PRECISA QUE A TABELA FORNECEDOR E FORNECEDOR MARCA ESTEJAM PREENCHIDAS
DELIMITER $$
drop procedure if exists pa_pesquisarFornecedorCod $$
create Procedure pa_pesquisarFornecedorCod(
	$cd_fornecedor int
)
main: begin
	select tbFornecedor.cd_fornecedor as 'Código Fornecedor', 
    nome as Nome, 
    sobrenome as Sobrenome, 
    email as 'E-mail', 
    no_tel as Telefone, 
    no_cnpj as CNPJ, 
    tbFornecedorMarca.nm_marca as 'Nome da marca' 
    from tbFornecedor
	inner join tbFornecedorMarca 
    on tbFornecedorMarca.cd_fornecedor = tbFornecedor.cd_fornecedor
	where tbFornecedor.cd_fornecedor = $cd_fornecedor;
end $$
DELIMITER ;









  /* -- PRODUTO*/ 
-- PRA FUNCIONAR PRECISA QUE A TABELA PRODUTO, ESTOQUE ESTEJAM PREENCHIDAS
DELIMITER $$
drop procedure if exists pa_pesquisarProduto $$

create Procedure pa_pesquisarProduto(
	$nm_marca varchar(50), 
	$tipo varchar(60)
)
main: begin
    select tbProduto.cd_produto as 'Código Produto',
    nm_marca as 'Nome da marca', 
    tipo as 'Tipo do produto', 
    descricao as 'Descrição do produto', 
    aspecto as 'Aspecto do produto', 
    vl_preco_unitario as 'Preço unitário',
    garantia as 'Garantia do produto', 
    caminho_imagem as 'Imagem do produto', 
    tbEstoque.qt_estoque as 'Quantidade em estoque' 
    from tbProduto
    inner join tbEstoque 
    on tbEstoque.cd_produto = tbProduto.cd_produto
    where nm_marca =  $nm_marca
    or tipo =  $tipo;
end$$
DELIMITER ;

-- call pa_pesquisarProduto('Carolina Herrera', '');




/* -- PRODUTO*/ 
DELIMITER $$
drop procedure if exists pa_pesquisarProduto2 $$
create Procedure pa_pesquisarProduto2(
	$nm_marca varchar (50),
	$tipo varchar (60),
	$descricao varchar (255), 
	$aspecto varchar (255),
	$precoMin decimal (10,2), 
	$precoMax decimal (10,2) 
)
main: begin
    select * from tbProduto
    where nm_marca like  concat('%', $nm_marca,'%')
    and tipo like  concat('%', $tipo,'%')
    and descricao like  concat('%', $descricao,'%')
    and aspecto  like concat('%', $aspecto,'%')
    and vl_preco_unitario between $precoMin and $precoMax;
end$$
DELIMITER ;

-- call pa_pesquisarProduto2('', 'sol', '', '', 0, 10000);




/* -- ESTOQUE*/ 
-- -------------
DELIMITER $$
drop procedure if exists pa_consultarQtdEstoque$$
create Procedure pa_consultarQtdEstoque (
	$cd_produto int
)
main: begin
	select qt_estoque from tbEstoque where cd_produto = cd_produto;
end$$
DELIMITER ;

-- call pa_consultarQtdEstoque(1);









/* -- PRODUTO*/ 
DELIMITER $$
drop procedure if exists pa_consultarMarcasOculos$$
create Procedure pa_consultarMarcasOculos ()
main: begin
	select f.nm_marca from tbFornecedorMarca as f 
	inner join tbProduto as p 
	on f.nm_marca = p.nm_marca 
	where p.tipo not like '%Lente%';
end$$
DELIMITER ;




/* -- PRODUTO*/ 
DELIMITER $$
drop procedure if exists pa_consultarMarcasLente$$
create Procedure pa_consultarMarcasLente ()
main: begin
	select p.cd_produto, 
    f.nm_marca, 
    p.descricao, 
    p.aspecto, 
    p.vl_preco_unitario, 
    p.garantia 
	from tbFornecedorMarca as f 
    inner join tbProduto as p 
    on f.nm_marca = p.nm_marca 
    where p.tipo like '%Lente%';
end$$
DELIMITER ;





/* -- PRODUTO*/ 
DELIMITER $$
drop procedure if exists pa_consultarTopPromo$$
create Procedure pa_consultarTopPromo ()
main: begin
	select * from tbproduto where tipo not like 'Lente' 
    ORDER BY vl_preco_unitario asc LIMIT 5;
end$$
DELIMITER ;





/* -- PEDIDO*/ 
DELIMITER $$
drop procedure if exists pa_ultimoPedidoCliente $$
create Procedure pa_ultimoPedidoCliente(
	$cd_cliente int
)
main: begin
	select * from tbPedido where cd_cliente = $cd_cliente 
    ORDER BY cd_pedido DESC LIMIT 1;
end$$
DELIMITER ;




-- EXIBIR PEDIDO 1 - PARA O CLIENTE
-- PRA FUNCIONAR PRECISA QUE A TABELA PEDIDO, PEDIDO ITENS E FORMA PAGAMENTO ESTEJAM PREENCHIDAS
DELIMITER $$
drop procedure if exists pa_exibirPedido1 $$
create procedure pa_exibirPedido1(
	$cd_cliente int
)
main: begin
    select DISTINCT a.cd_pedido as 'Codigo', 
    a.dt_pedido as 'Data do Pedido', 
    SUM(b.qt_item) as 'Qtde. de Itens',
    SUM(b.vl_subtotal) as 'Total'
    FROM tbPedido a
	LEFT JOIN tbPedidoItens b
	ON a.cd_pedido = b.cd_pedido
	where cd_cliente = $cd_cliente
    GROUP BY a.cd_pedido;
end $$
DELIMITER ;


/* -- PEDIDO*/ 
DELIMITER $$
drop procedure if exists pa_verificaPedidos $$
create Procedure pa_verificaPedidos(
	$cd_cliente int
)
main: begin
	select count(*) from tbPedido where cd_cliente = $cd_cliente;
end$$
DELIMITER ;

-- call pa_verificaPedidos(1);





-- PESQUISAR PEDIDORECEITA
-- PESQUISAR PELO CODIGO DO CLIENTE
DELIMITER $$
drop procedure if exists pa_pesquisarPedidoReceita $$
create procedure pa_pesquisarPedidoReceita(
	$cd_cliente int, 
	$cd_pedido int
)
main: begin
select pr.cd_pedido, 
r.cd_receita,
r.olho_direito, 
r.olho_esquerdo, 
r.distancia_pupilar, 
r.nm_oftalmo, 
r.sobrenome_oftalmo,
r.dt_receita, 
r.dt_validade, 
r.observacao 
from tbReceita as r
inner join tbPedidoReceita as pr
on r.cd_receita = pr.cd_receita
where cd_cliente = $cd_cliente or cd_pedido = $cd_pedido;

end $$
DELIMITER ;

-- call pa_pesquisarPedidoReceita(0, 3);






-- PESQUISAR FormaPagamento
-- PESQUISAR PELO CODIGO DO CLIENTE
DELIMITER $$
drop procedure if exists pa_pesquisarFormaPagamento $$
create procedure pa_pesquisarFormaPagamento(
$cd_cliente int, 
$cd_pedido int
)
main: begin
	select * from tbFormaPagamento 
	inner join tbPedido 
	on tbPedido.cd_pedido = tbFormaPagamento.cd_pedido
	where cd_cliente = $cd_cliente or tbPedido.cd_pedido = $cd_pedido;
end $$
DELIMITER ;







-- PESQUISAR PELO CODIGO DO CLIENTE
DELIMITER $$
drop procedure if exists pa_pesquisarFormaPagamentoCli $$
create procedure pa_pesquisarFormaPagamentoCli(
$cd_cliente int
)
main: begin
	select * from tbFormaPagamento 
	inner join tbPedido 
	on tbPedido.cd_pedido = tbFormaPagamento.cd_pedido
	where cd_cliente = $cd_cliente;
end $$
DELIMITER ;

-- call pa_pesquisarFormaPagamento(0, 3);




-- PESQUISAR PedidoItens
-- PESQUISAR PELO CODIGO DO CLIENTE
DELIMITER $$
drop procedure if exists pa_pesquisarPedidoItens $$
create Procedure pa_pesquisarPedidoItens(
$cd_cliente int, 
$cd_pedido int
)
main: begin
    select itens.cd_pedido as 'Codigo Pedido', 
    pro.caminho_imagem as imagem,
    concat(pro.nm_marca, ' ', pro.cd_produto) as 'Item', 
    pro.vl_preco_unitario as 'Preço Un.', 
    qt_item as 'Qtde.', 
    vl_subtotal as 'SubTotal', 
    pro.descricao, 
    pro.aspecto
    from tbPedidoItens as itens
    INNER JOIN tbProduto as pro
    on itens.cd_produto = pro.cd_produto
    inner join tbpedido as p
    on p.cd_pedido = itens.cd_pedido
    inner join tbcliente as c
    on c.cd_cliente = p.cd_cliente
    where p.cd_cliente = $cd_cliente or p.cd_pedido = $cd_pedido;
end$$
DELIMITER ;

-- call pa_pesquisarPedidoItens(3, 0);





-- EXIBIR PEDIDO VENDAS  -- PARA OS FUNCIONÁRIOS
DELIMITER $$
drop procedure if exists pa_exibirVendas $$
create procedure pa_exibirVendas()
main: begin
	select ped.cd_pedido as 'Código do Pedido', 
    cli.cd_cliente as 'Código do Cliente', 
    concat(cli.nome, ' ', cli.sobrenome) as 'Cliente', 
	dt_pedido as 'Data do Pedido', 
    p.parcelamento as 'Tipo de Parcelamento', 
    p.vl_total as 'Total'
	from tbPedido as ped 
	inner join tbFormaPagamento as p
	on ped.cd_pedido = p.cd_pedido
	inner join tbCliente as cli
	on ped.cd_cliente = cli.cd_cliente
    order by ped.cd_pedido desc;
end $$
DELIMITER ;






-- PESQUISAR PEDIDO VENDAS  -- PARA OS FUNCIONÁRIOS
DELIMITER $$
drop procedure if exists pa_pesquisarVendas $$
create procedure pa_pesquisarVendas(
	$nome varchar(30), 
	$sobrenome varchar(50), 
	$dt_pedido varchar(10), 
	$parcelamento varchar(40)
)
main: begin
	select ped.cd_pedido as 'Código do Pedido', 
    cli.cd_cliente as 'Código do Cliente', 
    concat(cli.nome, ' ', cli.sobrenome) as 'Cliente', 
	dt_pedido as 'Data do Pedido', 
    p.parcelamento as 'Tipo de Parcelamento', 
    p.vl_total as 'Total'
	from tbPedido as ped 
	inner join tbFormaPagamento as p
	on ped.cd_pedido = p.cd_pedido
	inner join tbCliente as cli
	on ped.cd_cliente = cli.cd_cliente
    where cli.nome like concat('%', $nome,'%')
    and cli.sobrenome like concat('%', $sobrenome,'%')
    and dt_pedido like concat('%', $dt_pedido,'%')
    and p.parcelamento like concat('%', $parcelamento,'%')
    ;
end $$
DELIMITER ;

-- call pa_pesquisarVendas('', '','','3');







/* -- NOTIFICACAO*/ 
DELIMITER $$
drop procedure if exists pa_verificaNotificacao $$
create Procedure pa_verificaNotificacao(
	$cd_cliente int
)
main: begin
	select f.cd_produto, f.cd_notificacao 
    from tbProdutoNotificacao as f
	inner join tbEstoque as e 
	on f.cd_produto = e.cd_produto
	where e.qt_estoque > 0
	and cd_cliente = $cd_cliente;
end$$
DELIMITER ;





/* -- CARRINHO*/
DELIMITER $$
drop procedure if exists pa_pesquisarCarrinhoCompra $$
create Procedure pa_pesquisarCarrinhoCompra(
	$cd_cliente int
)
main: begin
    select distinct 
    cd_carrinho, 
    pro.cd_produto, 
    pro.caminho_imagem as imagem , 
    concat(pro.nm_marca, ' ', pro.cd_produto) as 'Item', 
    pro.vl_preco_unitario as 'Preço Un.', 
    qt_item as 'Qtde.' 
    from tbCarrinhoCompra as car
    inner JOIN tbProduto as pro
    on car.cd_produto = pro.cd_produto
    where cd_cliente = $cd_cliente;
end$$
DELIMITER ;

-- call pa_pesquisarCarrinhoCompra(3);





/* -- CARRINHO*/ 
DELIMITER $$
drop procedure if exists pa_verificaCarrinhoCompra $$
create Procedure pa_verificaCarrinhoCompra (
	$cd_cliente int
)
main: begin
	select count(*) from tbCarrinhoCompra where cd_cliente = $cd_cliente;
end$$
DELIMITER ;


-- ---------------------------------

DELIMITER $$
drop procedure if exists pa_verificaEstoque $$
create Procedure pa_verificaEstoque (
	$cd_produto int
)
main: begin
	select qt_estoque from tbEstoque 
where cd_produto = cd_produto;
end$$
DELIMITER ;

-- ---------------------------------------------------
DELIMITER $$
drop procedure if exists pa_exibirCarrinhoCompra $$
create Procedure pa_exibirCarrinhoCompra(
	$cd_cliente int
)
main: begin
    select DISTINCT pro.descricao as 'Item', 
    pro.vl_preco_unitario as 'Preço Un.', 
    qt_item as 'Qtde.', 
    pro.vl_preco_unitario * qt_item as 'Total'
    from tbCarrinhoCompra as car
    INNER JOIN tbProduto as pro
    on car.cd_produto = pro.cd_produto
    where cd_cliente = $cd_cliente;
end$$
DELIMITER ;

call pa_exibirCarrinhoCompra(1);


-- -----------------------------------------

DELIMITER $$
drop procedure if exists pa_ultimoReceitaCliente $$
create Procedure pa_ultimoReceitaCliente(
	$cd_cliente int
)
main: begin
	select * from tbReceita where cd_cliente = cd_cliente 
    ORDER BY cd_receita DESC LIMIT 1;
end$$
DELIMITER ;

-- -----------------------------------------

DELIMITER $$
drop procedure if exists pa_ultimoCarrinhoCliente $$
create Procedure pa_ultimoCarrinhoCliente(
	$cd_cliente int
)
main: begin
	select cd_carrinho from tbcarrinhocompra 
    where cd_cliente = $cd_cliente ORDER BY cd_carrinho DESC LIMIT 1;
end$$
DELIMITER ;

-- -----------------------------------------
/* -- ESTOQUE DIMINUIR QT EM ESTOQUE*/
DELIMITER $$
drop procedure if exists pa_diminuiEstoque $$
create Procedure pa_diminuiEstoque($qt_itens int, $cd_produto int)
main: begin
	update tbEstoque
    set qt_estoque = qt_estoque - $qt_itens
    where cd_produto = $cd_produto;
end$$
DELIMITER ;