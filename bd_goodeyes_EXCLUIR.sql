/*
	PROCEDURES DO BANCO DE DADOS GOODEYES
*/

/* -- LOGIN*/
DELIMITER $$    
drop procedure if exists pa_deleteLogin$$    
create procedure pa_deleteLogin($email varchar (60)) 
main: begin 
    delete from tbCliente where email = $email;

    delete from tbFuncionario where email = $email;

    delete from tbLogin where email = $email;

end$$
DELIMITER ;

-- call pa_deleteLogin('jorge.carneiro@outlook.com');





/* -- CLIENTE*/
DELIMITER $$
drop procedure if exists pa_deleteCliente$$  
create Procedure pa_deleteCliente($cd_cliente int)
main: begin 
    delete from tbPedidoReceita 
    where cd_receita in (select cd_receita from tbReceita where cd_cliente = $cd_cliente);

    delete from tbReceita
    where $cd_cliente = $cd_cliente;

    delete from tbFormaPagamento
    where cd_pedido in (select cd_pedido from tbPedido where cd_cliente = $cd_cliente );

    delete from tbPedidoItens
    where cd_pedido in (select cd_pedido from tbPedido where cd_cliente = $cd_cliente ); 

    delete from tbPedido
    where cd_cliente = $cd_cliente;

    delete from tbProdutoNotificacao
    where cd_cliente = $cd_cliente;

    delete from tbCarrinhoCompra
    where cd_cliente = $cd_cliente;

    delete from tbLogin
    where email in (select email from tbCliente where cd_cliente = $cd_cliente);

    delete from tbCliente
    where cd_cliente = $cd_cliente;
end$$
DELIMITER ;

-- call pa_deleteCliente(1);






-- ------------FUNCIONÁRIO DELETE---------------
DELIMITER $$
drop procedure if exists pa_deleteFuncionario$$  
create Procedure pa_deleteFuncionario($cd_funcionario int)
main: begin 
    delete from tbLogin
    where email in (select email from tbFuncionario where cd_funcionario = $cd_funcionario);

    delete from tbFuncionario
    where cd_funcionario = $cd_funcionario;
end$$
DELIMITER ;

-- call pa_deleteFuncionario(2);


-- call pa_exibirFuncionario;



-- ------------FORNECEDOR DELETE---------------
DELIMITER $$
drop procedure if exists pa_deleteFornecedor$$
Create Procedure pa_deleteFornecedor($cd_fornecedor int)
main: begin 

    delete from tbEstoque
    where cd_produto in (select cd_produto from tbProduto where nm_marca in 
    (select nm_marca from tbFornecedorMarca where cd_fornecedor = $cd_fornecedor));
   
    delete FROM tbPedidoReceita
    where cd_pedido in (select cd_pedido from tbPedidoItens where cd_produto in
    (select cd_produto from tbProduto where nm_marca in 
    (select nm_marca from tbFornecedorMarca where cd_fornecedor = $cd_fornecedor))
    );

    delete from tbFormaPagamento
    where cd_pedido in (select cd_pedido from tbPedidoItens where cd_produto in
    (select cd_produto from tbProduto where nm_marca in 
    (select nm_marca from tbFornecedorMarca where cd_fornecedor = $cd_fornecedor))
    );

    delete from tbPedidoItens
    where cd_produto in (select cd_produto from tbProduto where nm_marca in 
    (select nm_marca from tbFornecedorMarca where cd_fornecedor = $cd_fornecedor));

    delete from tbPedido
    where cd_pedido not in (select cd_pedido from tbPedidoItens);

    delete from tbProdutoNotificacao
    where cd_produto in (select cd_produto from tbProduto where nm_marca in 
    (select nm_marca from tbFornecedorMarca where cd_fornecedor = $cd_fornecedor));

    delete from tbCarrinhoCompra
    where cd_produto in (select cd_produto from tbProduto where nm_marca in 
    (select nm_marca from tbFornecedorMarca where cd_fornecedor = $cd_fornecedor));

    delete from tbProduto
    where nm_marca in (select nm_marca from tbFornecedorMarca where cd_fornecedor = $cd_fornecedor);

    delete from tbFornecedorMarca
    where cd_fornecedor = $cd_fornecedor;

    delete from tbFornecedor
    where cd_fornecedor = $cd_fornecedor;

end$$
DELIMITER ;

-- call pa_deleteFornecedor(1);




-- ------------FORNECEDOR MARCA DELETE---------------
DELIMITER $$
drop procedure if exists pa_deleteFornecedorMarca $$
create Procedure pa_deleteFornecedorMarca($nm_marca varchar (50))
main: begin

    delete FROM tbEstoque
    where cd_produto in (select cd_produto from tbProduto where nm_marca = $nm_marca);
    
    delete from tbFormaPagamento
    where cd_pedido in (select cd_pedido from tbPedidoItens where cd_produto in
    (select cd_produto from tbProduto where nm_marca = $nm_marca));

    delete FROM tbPedidoReceita
    where cd_pedido in (select cd_pedido from tbPedidoItens where cd_produto in
    (select cd_produto from tbProduto where nm_marca = $nm_marca));

    delete FROM tbPedidoItens
    where cd_produto in (select cd_produto from tbProduto where nm_marca = $nm_marca);

    delete from tbPedido
    where cd_pedido not in (select cd_pedido from tbPedidoItens);

    delete FROM tbProdutoNotificacao
    where cd_produto in (select cd_produto from tbProduto where nm_marca = $nm_marca);

    delete FROM tbCarrinhoCompra
    where cd_produto in (select cd_produto from tbProduto where nm_marca = $nm_marca);

    delete FROM tbProduto
    where nm_marca = $nm_marca;

    delete FROM tbFornecedorMarca
    where nm_marca = $nm_marca;    

end$$
DELIMITER ;

-- call pa_deleteFornecedorMarca('Armani');






-- ------------RECEITA DELETE---------------
DELIMITER $$
create Procedure pa_deleteReceita($cd_receita int)
MAIN: begin

    delete FROM tbPedidoReceita
    where cd_receita = $cd_receita;

    delete FROM tbReceita
    where cd_receita = $cd_receita;

end$$
DELIMITER ;

-- call pa_deleteReceita(3);





-- ---------PRODUTO DELETE-----------------------
DELIMITER $$
create Procedure pa_deleteProduto($cd_produto int)
MAIN: begin

    delete FROM tbEstoque
    where cd_produto = $cd_produto;

    delete from tbFormaPagamento
    where cd_pedido in (select cd_pedido from tbPedidoItens where cd_produto = $cd_produto);

    delete FROM tbPedidoReceita
    where cd_pedido in (select cd_pedido from tbPedidoItens where cd_produto = $cd_produto);

    delete FROM tbPedidoItens
    where cd_produto = $cd_produto;

    delete from tbPedido
    where cd_pedido not in (select cd_pedido from tbPedidoItens);

    delete FROM tbProdutoNotificacao
    where cd_produto = $cd_produto;

    delete FROM tbCarrinhoCompra
    where cd_produto = $cd_produto;

    delete FROM tbProduto
    where cd_produto = $cd_produto;    

end$$
DELIMITER ;

-- call pa_deleteProduto(3);







-- ----------PEDIDO DELETE----------
DELIMITER $$
create Procedure pa_deletePedido($cd_pedido int)
main: begin

    delete FROM tbFormaPagamento
    where cd_pedido = $cd_pedidov;

    delete FROM tbPedidoReceita
    where cd_pedido = $cd_pedido;

    delete FROM tbPedidoItens
    where cd_pedido = $cd_pedido;

    delete FROM tbPedido
    where cd_pedido = $cd_pedido;

end$$
DELIMITER ;

-- call pa_deletePedido(3);









-- -----------FORMA PAGAMENTO DELETE------------
DELIMITER $$
create Procedure pa_deleteFormaPagamento($cd_pagamento int)
MAIN: begin
    delete FROM tbFormaPagamento
    where cd_pagamento = $cd_pagamento;
end$$
DELIMITER ;

-- call pa_deleteFormaPagamento(3);





-- -----------ESTOQUE DELETE-------------
DELIMITER $$
create Procedure pa_deleteEstoque(cd_estoque int)
MAIN: begin
    delete FROM tbEstoque
    where cd_estoque = $cd_estoque;
end$$
DELIMITER ;

-- call pa_deleteEstoque(2);







-- ----------PEDIDO ITENS DELETE---------------
DELIMITER $$
create Procedure pa_deletePedidoItens($cd_item int)
MAIN: begin
    delete FROM tbPedidoItens
    where cd_item = $cd_item;
end$$
DELIMITER ;

-- call pa_deletePedidoItens(1);






-- -----PEDIDO RECEITA DELETE-------------
DELIMITER $$
create Procedure pa_deletePedidoReceita($cd_pedidoreceita int)
MAIN: begin
    delete FROM tbPedidoReceita
    where cd_pedidoreceita = $cd_pedidoreceita;
end$$
DELIMITER ;

-- call pa_deletePedidoReceita(3);







-- ---------FAVORITOS DELETE-----------------------
DELIMITER $$
create Procedure pa_deleteNotificacao($cd_notificacao int)
MAIN: begin
    delete FROM tbProdutoNotificacao
    where cd_notificacao = cd_notificacao;
end$$
DELIMITER ;

-- call pa_deleteNotificacao(3);






-- --------CARRINHO DE COMPRAS DELETE-----------------------
DELIMITER $$
drop procedure if exists pa_deleteCarrinhoCompra $$   

create Procedure pa_deleteCarrinhoCompra($cd_cliente int)
MAIN: begin
    delete FROM tbCarrinhoCompra
    where cd_cliente = $cd_cliente;
end$$
DELIMITER ;
-- --------CARRINHO DE COMPRAS DELETE-----------------------

DELIMITER $$
drop procedure if exists pa_deleteCarrinhoCompra2 $$   

create Procedure pa_deleteCarrinhoCompra2($cd_carrinho int)
MAIN: begin
    delete FROM tbCarrinhoCompra
    where cd_carrinho = $cd_carrinho;
end$$
DELIMITER ;

-- call pa_deleteCarrinhoCompra(3);





-- -----CARRINHO RECEITA DELETE-------------
DELIMITER $$
drop procedure if exists pa_deleteCarrinhoReceitaCliente $$ 

create Procedure pa_deleteCarrinhoReceitaCliente($cd_cliente int)
MAIN: begin
    delete FROM tbCarrinhoReceita
    where cd_cliente  = $cd_cliente;
end$$
DELIMITER ;


-- -----CARRINHO RECEITA DELETE-------------
DELIMITER $$
drop procedure if exists pa_deleteCarrinhoReceita $$ 

create Procedure pa_deleteCarrinhoReceita($cd_carrinhoreceita int)
MAIN: begin
    delete FROM tbCarrinhoReceita
    where cd_carrinhoreceita  = $cd_carrinhoreceita;
end$$
DELIMITER ;




