/*
	PROCEDURES DO BANCO DE DADOS GOODEYES
*/

/*
	**EXIBIR**********************************************************************
*/

/* -- LOGIN*/
DELIMITER $$
drop procedure if exists pa_exibirLogin $$
create Procedure pa_exibirLogin ()
main: begin
	select email as 'E-mail', senha as Senha, nivel_acesso as 'Nível de acesso' from tbLogin;
end $$
DELIMITER ;

-- call pa_exibirLogin()

/* -- CLIENTE*/
DELIMITER $$
drop procedure if exists pa_exibirCliente $$
create Procedure pa_exibirCliente()
main: begin
	select * from tbCliente;
end$$
DELIMITER ;


/* -- RECEITA*/
DELIMITER $$
drop procedure if exists pa_exibirReceita $$
create Procedure pa_exibirReceita()
main: begin
	select * from tbReceita;
end $$
DELIMITER ;


/* -- FUNCIONÁRIO*/
DELIMITER $$
drop procedure if exists pa_exibirFuncionario $$
create Procedure pa_exibirFuncionario()
main: begin
	select * from tbFuncionario;
end$$
DELIMITER ;




/* -- FORNECEDOR*/
DELIMITER $$
drop procedure if exists pa_exibirFornecedor $$
create Procedure pa_exibirFornecedor()
main: begin
	select * from tbFornecedor;
end$$
DELIMITER ;





/* -- FORNECEDOR MARCA*/
DELIMITER $$
drop procedure if exists pa_exibirFornecedorMarca $$
create Procedure pa_exibirFornecedorMarca()
main: begin
	select nm_marca as 'Nome da marca', tbfornecedor.cd_fornecedor as 'Código Fornecedor', nome as 'Nome do Fornecedor',
    sobrenome as 'Sobrenome do Fornecedor'
    from tbFornecedorMarca
    inner join tbFornecedor
    on tbfornecedor.cd_fornecedor = tbfornecedormarca.cd_fornecedor;
end $$
 DELIMITER ;
 
 
 
/* -- PRODUTO*/     
DELIMITER $$
drop procedure if exists pa_exibirProduto $$
create Procedure pa_exibirProduto ()
main: begin
	select * from tbProduto;
end$$
DELIMITER $$




/* -- PRODUTO MAIS VENDIDOS*/ 
DELIMITER $$
drop procedure if exists pa_produtosMaisVendidos $$
create Procedure pa_produtosMaisVendidos ()
main: begin
	select p.nm_marca as Produto, p.vl_preco_unitario as 'Preço Unitário',  Count(itens.qt_item) as 'Vendas'
	from tbProduto as p
	inner join tbPedidoItens as itens
	on p.cd_produto = itens.cd_produto 
	order by itens.qt_item 
	desc limit 5;
end$$
DELIMITER ;





/* -- PRODUTO MENOS VENDIDOS*/ 
DELIMITER $$
drop procedure if exists pa_produtosMenosVendidos $$
create Procedure pa_produtosMenosVendidos ()
main: begin
	select p.nm_marca as Produto, p.vl_preco_unitario as 'Preço Unitário', 
	Count(itens.qt_item) as 'Vendas'
	from tbProduto as p
	inner join tbPedidoItens as itens
	on p.cd_produto = itens.cd_produto 
	order by itens.qt_item  
	asc limit 5;
end$$
DELIMITER ;





/* -- PRODUTO FORA DE ESTOQUE*/ 
DELIMITER $$
drop procedure if exists pa_produtosForaEstoque $$
create Procedure pa_produtosForaEstoque ()
main: begin
	select p.nm_marca as Produto
	from tbProduto as p
	inner join tbEstoque as e
	on p.cd_produto = e.cd_produto
	where e.qt_estoque = 0;
end$$
DELIMITER ;






/* -- ESTOQUE*/ 
DELIMITER $$
drop procedure if exists pa_exibirEstoque $$
create Procedure pa_exibirEstoque()
main: begin
	select cd_estoque as 'Código estoque', cd_produto as 'Código Produto', qt_estoque as 'Quantidade estoque' from tbEstoque;
end$$
DELIMITER ;






/* -- PEDIDO*/ 
DELIMITER $$
drop procedure if exists pa_exibirPedido $$
create Procedure pa_exibirPedido()
main: begin
	select cd_pedido as 'Código pedido', cd_cliente as 'Código cliente', dt_pedido as 'Data do pedido' from tbPedido;
end$$
DELIMITER ;






/* -- PEDIDO ITENS*/ 
DELIMITER $$
drop procedure if exists pa_exibirPedidoItens $$
create Procedure pa_exibirPedidoItens()
main: begin
	select cd_item as 'Código pedido itens', cd_pedido 'Código pedido', 
    cd_produto as 'Código produto', qt_item as 'Quantidade pedido itens', 
    vl_subtotal as 'Subtotal' 
    from tbPedidoItens;
end$$
DELIMITER ;




/* -- PEDIDO RECEITA*/ 
DELIMITER $$
drop procedure if exists pa_exibirPedidoReceita $$
create Procedure pa_exibirPedidoReceita()
main: begin
	select cd_pedidoreceita as 'Código Pedido Receita', 
    cd_pedido as 'Código do Pedido', 
    cd_receita as 'Código da Receita' 
    from tbPedidoReceita;
end$$
DELIMITER ;





/* -- PAGAMENTO*/ 
DELIMITER $$
create Procedure pa_exibirFormaPagamento()
main: begin
	select cd_pagamento as 'Código Pagamento', cd_pedido as 'Código Pedido', 
    tipo_pagamento as 'Tipo pagamento', parcelamento as 'Tipo parcelamento',
    vl_total as 'Valor total' from tbFormaPagamento;
end $$
DELIMITER ;







/* -- NOTIFICACAO*/ 
DELIMITER $$
create Procedure pa_exibirNotificacao()

main: begin
	select cd_notificacao as 'Código Favoritos', cd_cliente as 'Código cliente', cd_produto as 'Código Produto' from tbProdutoNotificacao;
end$$
DELIMITER ;




