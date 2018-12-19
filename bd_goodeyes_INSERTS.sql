/*
	PROCEDURES DO BANCO DE DADOS GOODEYES
*/


/* 
	**INSERIR*********************************************
*/	


/* -- LOGIN*/
DELIMITER $$
drop procedure if exists pa_inserirLogin $$	
create procedure pa_inserirLogin (
	$email varchar (60),
	$senha varchar (8), 
	$nivel_acesso int
	)
main: begin 
	insert into tbLogin (email, senha, nivel_acesso)
	values ($email, $senha, $nivel_acesso);
end$$
DELIMITER ;

call pa_inserirLogin ('ana.silva@outlook.com','a', 1);
call pa_inserirLogin ('jorge.carneiro@outlook.com','jor12456', 1);
call pa_inserirLogin ('maria.veloso@outlook.com','maveloso', 1);
call pa_inserirLogin ('julia.barbosa@outlook.com', 'barbosa',2);
call pa_inserirLogin ('leticia.vieira@outlook.com', '12345678',2);
call pa_inserirLogin ('carla.silva@outlook.com','carla',3);
call pa_inserirLogin ('martha.martins@outlook.com','adm45678',3);




/* -- CLIENTE*/
DELIMITER $$
drop procedure if exists pa_inserirCliente $$
create Procedure pa_inserirCliente (
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
	insert into tbCliente (
		email, nome, sobrenome, no_cpf, 
		no_tel, no_cel, dt_nascimento,
		nm_rua, no_rua, no_cep, bairro, cidade, 
		estado, sg_uf, complemento
		) 
	values (
		$email, $nome, $sobrenome, $no_cpf, 
		$no_tel, $no_cel, $dt_nascimento,
		$nm_rua, $no_rua, $no_cep, $bairro, $cidade, 
		$estado, $sg_uf, $complemento
		);
end$$
DELIMITER ;


call pa_inserirCliente  ('ana.silva@outlook.com','Ana', 'Silva', '411.171.988-02', '(11)3947-5213', '(11)96554-2536', '02/06/1990', 'Rua 2', '45', '02990-270', 'Jardim Amélia','Pirapora', 'São Paulo', 'SP', 'apt A');
call pa_inserirCliente  ('jorge.carneiro@outlook.com','Jorge', 'Carneiro', '411.557.911-05', '(11)3947-5758', '(11)91236-2756',  '08/05/1995', 'Rua 5', '15', '02990-111', 'Jardim Rosa', 'Santos','São Paulo', 'SP', 'apt C');
call pa_inserirCliente  ('maria.veloso@outlook.com','Maria', 'Veloso', '322.222.999-04', '(11)3947-5758', '(11)91236-2756',  '01/05/1996', 'Rua 7', '02', '02981-222', 'Jardim Peri', 'Jundiai','São Paulo', 'SP', 'apt F');





/* -- RECEITA*/
DELIMITER $$
drop procedure if exists pa_inserirReceita $$
create Procedure pa_inserirReceita (
	$cd_cliente  int,
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
	insert into tbReceita (
		cd_cliente, olho_direito, olho_esquerdo,
		distancia_pupilar, nm_oftalmo, sobrenome_oftalmo,
		dt_receita, dt_validade, observacao
        )
	values (
		$cd_cliente, $olho_direito, $olho_esquerdo,
		$distancia_pupilar, $nm_oftalmo, $sobrenome_oftalmo,
		$dt_receita, $dt_validade, $observacao
	);
end$$
DELIMITER ;

call pa_inserirReceita (1,'+4,50 -2,00 180° +1', '+2,50 -2,00 180° +1', '10mm','Laura', 'Fernandes', '15/04/2018', '15/05/2018', 'Lente bifocal');
call pa_inserirReceita (2, '2 0 0 0', '-1,50 0 0 0','10mm',  'Ricardo', 'Freitas', '10/06/2018', '10/07/2018', 'Lente transitions');
call pa_inserirReceita (3, '0 0 0 0', '-1,10 0 0 0','15mm',  'Paulo', 'Avelino', '02/09/2018', '02/09/2018', 'Lente transitions');







/* -- FUNCIONÁRIO*/
DELIMITER $$
drop procedure if exists pa_inserirFuncionario $$
create Procedure pa_inserirFuncionario (
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
	insert into tbFuncionario (
    email, nome, sobrenome,
	no_cpf,	cargo, no_tel,
	no_cel, dt_nascimento, nm_rua,
	no_rua, no_cep,	bairro,
	cidade,	estado,	sg_uf,
	complemento
	)
	values (
    $email, $nome, $sobrenome,
    $no_cpf, $cargo, $no_tel, 
    $no_cel, $dt_nascimento, $nm_rua, 
    $no_rua, $no_cep, $bairro, 
    $cidade, $estado, $sg_uf,
    $complemento
    );
end $$
DELIMITER ;


call pa_inserirFuncionario  ('julia.barbosa@outlook.com','Julia', 'Santos Barbosa', '254.569.744-08', 'Atendente', '(11) 6598-4521', '(11) 96532-7854', '13/06/1989', 'Rua 1', '02', '02555-222', 'Av. Ramos', 'Vinhedo','São Paulo', 'SP', 'nenhum');
call pa_inserirFuncionario  ('leticia.vieira@outlook.com','Leticia', 'Vieira Lemos', '145.456.984-01',  'Recepcionista', '(11) 9965-4569', '(11) 96532-1111', '03/01/1990', 'Rua 03', '25', '03040-256', 'Jardim Perdizes','Santo André', 'São Paulo', 'SP', 'apt F');
call pa_inserirFuncionario  ('carla.silva@outlook.com', 'Carla', 'Veloso Silva', '122.555.999-04', 'Gerente', '(11) 9111-4449', '(11) 96555-2211', '09/09/1990', 'Rua 07', '40', '03040-522', 'Jardim Pedra', 'Vinhedo','São Paulo', 'SP', 'apt E');
call pa_inserirFuncionario  ('martha.martins@outlook.com', 'martha', 'martins', '852.452.949-12', 'Gerente', '(11) 4212-3521', '(11) 94512-4511', '12/10/1982', 'Rua de Paula', '40', '8425-522', 'Jardim Moreira', 'Lopes','São Paulo', 'SP', 'apt E');









/* -- FORNECEDOR*/
DELIMITER $$
drop procedure if exists pa_inserirFornecedor $$
create Procedure pa_inserirFornecedor (
	$nome varchar(30),
	$sobrenome varchar(50),
	$email varchar(50),
	$no_tel varchar(14),
	$no_cnpj varchar(18)
)
main: begin
	insert into tbFornecedor (
		nome,
		sobrenome, email,
		no_tel, no_cnpj
    )
	values (
	$nome,
	$sobrenome,	$email,	
    $no_tel, $no_cnpj
    );
end $$
DELIMITER ;


call pa_inserirFornecedor ('Rafael', 'Costa', 'rafael.costa@outlook.com', '(11)3655-2010', '11.245.578/0001-30');
call pa_inserirFornecedor ('Roberto', 'Ribeiro', 'roberto.ribeiro@outlook.com', '(11)3775-4526', '12.255.511/0001-50');
call pa_inserirFornecedor ('Jorge', 'Ferreira', 'jorge.ferreira@outlook.com', '(11)3775-4526', '90.520.623/0001-50');
call pa_inserirFornecedor ('Alice', 'Machado', 'alicemachado@outlook.com', '(11)8754-9560', '24.085.104/0001-50');
call pa_inserirFornecedor ('Luisa', 'Carla Pereira', 'lup@outlook.com', '(11)3545-2526', '52.910.065/0001-50');
call pa_inserirFornecedor ('Alex', 'Romero', 'alexro@outlook.com', '(11)012-4806', '36.275.511/0001-50');
call pa_inserirFornecedor ('Sebastião', 'Silva', 'sebastian@outlook.com', '(11)3475-4326', '17.024.511/0001-50');




/* -- FORNECEDOR MARCA*/
DELIMITER $$
drop procedure if exists pa_inserirFornecedorMarca $$
create Procedure pa_inserirFornecedorMarca (
	$nm_marca varchar(50), 
	$cd_fornecedor int
)
main: begin
	insert into tbFornecedorMarca (nm_marca, cd_fornecedor)
	values ($nm_marca, $cd_fornecedor);
end$$
DELIMITER ;

call pa_inserirFornecedorMarca ('Chilli Beans', 1);
call pa_inserirFornecedorMarca ('Carolina Herrera', 2);
call pa_inserirFornecedorMarca ('Adidas', 3);
call pa_inserirFornecedorMarca ('Fendi', 4);
call pa_inserirFornecedorMarca ('Polaroid', 5);
call pa_inserirFornecedorMarca ('GUESS', 6);
call pa_inserirFornecedorMarca ('Havana', 7);
call pa_inserirFornecedorMarca ('BrowStripes', 6);
call pa_inserirFornecedorMarca ('Atitude', 7);
call pa_inserirFornecedorMarca ('Havaianas', 3);
call pa_inserirFornecedorMarca ('Armani', 4);
call pa_inserirFornecedorMarca ('Klipling', 5);
call pa_inserirFornecedorMarca ('Rayban', 1);
call pa_inserirFornecedorMarca ('Oakley', 2);
call pa_inserirFornecedorMarca ('Teaser', 2);
call pa_inserirFornecedorMarca ('Hip7', 2);
call pa_inserirFornecedorMarca ('Crizal', 6);
call pa_inserirFornecedorMarca ('Biofinty', 7);
call pa_inserirFornecedorMarca ('Acuvue', 7);
call pa_inserirFornecedorMarca ('Solotica', 3);
call pa_inserirFornecedorMarca ('Natural Vision', 4);


 



 /* -- PRODUTO*/
DELIMITER $$
drop procedure if exists pa_inserirProduto $$
create Procedure pa_inserirProduto (
$nm_marca varchar(50),
$tipo varchar(60),
$descricao varchar(255), 
$aspecto varchar(255),
$vl_preco_unitario decimal(10,2),
$garantia varchar(20),
$caminho_imagem varchar(255)
)
main: begin
	insert into tbProduto (
	nm_marca,
	tipo,
	descricao, 
	aspecto,
	vl_preco_unitario,
	garantia,
	caminho_imagem
    )
	values (
	$nm_marca,
	$tipo,
	$descricao, 
	$aspecto,
	$vl_preco_unitario,
	$garantia,
	$caminho_imagem
    );
end $$
DELIMITER ;

-- oculos de sol FEMININO
call pa_inserirProduto ('Carolina Herrera','Óculos de Sol', 'Madeira Redondo Feminino 15-25 Marrom Polarizada Preto', '22cm 135mm 53mm', 300.00,'1 ano', '/imagemProdutos/fendigatinho.jpg');
call pa_inserirProduto ('Adidas','Óculos de Sol', 'Acetato Quadrado Feminino 25-35 Preto Degradê Preto','20cm 135mm 40mm', 600.00,'6 meses', '/imagemProdutos/sol.jpg');
call pa_inserirProduto ('Fendi','Óculos de Sol', 'Metal Redondo Feminino 15-25 Rosa Espelhada Rosa', '30cm 150mm 40mm', 350.00,'1 ano', '/imagemProdutos/fendiRosaEspelhadoRosaGatinho.jpg');
call pa_inserirProduto ('GUESS','Óculos de Sol', 'Ácrilico Gatinho Feminino 46-65 Roxo Degradê Preto','15cm 130mm 38mm', 384.00,'6 meses', '/imagemProdutos/guessFem384.jpg');
call pa_inserirProduto ('BrowStripes','Óculos de Sol', 'Acetato Quadrado Feminino 35-45 Marrom Degradê Marrom', '14cm 150mm 37mm', 94.90,'1 ano', '/imagemProdutos/brownstripesFem94.jpg');
call pa_inserirProduto ('Havana','Óculos de Sol', 'Ácrilico Quadrado Feminino 5-10 Preto Degradê Preto','15cm 110mm 38mm', 600.00,'6 meses', '/imagemProdutos/havanaFem352.jpg');
call pa_inserirProduto ('Havaianas','Óculos de Sol', 'Acetato Quadrado Feminino 15-25 Rosa Degradê Preto','15cm 16mm 38mm', 212.00,'6 meses', '/imagemProdutos/havaianasFem212.jpg');
call pa_inserirProduto ('Rayban','Óculos de Sol', 'Metal Redondo Feminino 15-25 Rosa Espelhado Rosa','15cm 16mm 38mm', 212.00,'6 meses', '/imagemProdutos/rayban50065.jpg');

-- OCULOS DE SOL MASCULINO
call pa_inserirProduto ('Polaroid','Óculos de Sol', 'Acetato Quadrado Masculino 10-15 Azul Polarizada Preto','10cm 140mm 38mm', 141.00,'1 ano', '/imagemProdutos/polaroidFem141.jpg');
call pa_inserirProduto ('Armani','Óculos de Sol', 'Ácrilico Quadrado Masculino 25-35 Azul Polarizada Transparente','15cm 130mm 35mm', 380.50,'1 ano', '/imagemProdutos/armaniMas380.jpg');
call pa_inserirProduto ('Havaianas','Óculos de Sol', 'Acetato Aviador Masculino 25-35 Dourado Degradê Preto','15cm 16mm 38mm', 475.00,'6 meses', '/imagemProdutos/raybanFem475.jpg');
call pa_inserirProduto ('Havana','Óculos de Sol', 'Metal Aviador Masculino 25-35 Preto Normal Azul','15cm 16mm 38mm', 500.00,'1 ano', '/imagemProdutos/solmas500.jpg');
call pa_inserirProduto ('BrowStripes','Óculos de Sol', 'Acetato Quadrado Masculino 35-45 Preto Degradê Preto','15cm 16mm 38mm', 219.90,'6 meses', '/imagemProdutos/solmas219.jpg');
call pa_inserirProduto ('GUESS','Óculos de Sol', 'Acetato Gatinho Masculino 45-65 Preto Normal Preto','15cm 16mm 38mm', 419.00,'1 ano', '/imagemProdutos/solmas419.jpg');
call pa_inserirProduto ('Fendi','Óculos de Sol', 'Metal Redondo Masculino 25-35 Preto Degradê Verde','15cm 16mm 38mm', 442.00,'6 meses', '/imagemProdutos/solmas442.jpg');
call pa_inserirProduto ('Rayban','Óculos de Sol', 'Acetato Redondo Masculino 45-65 Preto Degradê Verde','15cm 16mm 38mm', 329.00,'6 meses', '/imagemProdutos/rayban352.jpg');
call pa_inserirProduto ('Rayban','Óculos de Sol', 'Acetato Redondo Masculino 45-65 Azul Degradê Preto','15cm 16mm 38mm', 329.00,'6 meses', '/imagemProdutos/solmas84.jpg');

-- OCULOS DE GRAU FEMININO
call pa_inserirProduto ('Klipling','Óculos de Grau', 'Metal Redondo Feminino 5-10 Azul Normal Transparente', '14cm 120mm 28mm', 257.00,'1 ano', '/imagemProdutos/kipling257.jpg');
call pa_inserirProduto ('Oakley','Óculos de Grau', 'Acetato Quadrado Feminino 25-35 Preto Normal Transparente','15cm 125mm 40mm', 387.00,'1 ano', '/imagemProdutos/oakley387.jpg');
call pa_inserirProduto ('Teaser','Óculos de Grau', 'Ácrilico Redondo Feminino 35- Branco Normal Transparente', '14cm 115mm 45mm', 94.00,'1 ano', '/imagemProdutos/teaserBranco94.jpg');
call pa_inserirProduto ('Teaser','Óculos de Grau', 'Ácrilico Redondo Feminino 15-25 Preto Normal Transparente', '14cm 115mm 45mm', 94.00,'1 ano', '/imagemProdutos/teaserPreto94.jpg');
call pa_inserirProduto ('Chilli Beans','Óculos de Grau', 'Metal Aviador Feminino 35-45 Dourado Normal Transparente','15cm 130mm 38mm', 500.00,'6 meses', '/imagemProdutos/grau.jpg');
call pa_inserirProduto ('Klipling','Óculos de Grau', 'Acetato Quadrado Feminino 5-10 Branco Normal Transparente', '14cm 120mm 28mm', 257.00,'1 ano', '/imagemProdutos/teaser1850.jpg');
call pa_inserirProduto ('Oakley','Óculos de Grau', 'Acetato Quadrado Feminino 25-35 Vermelho Normal Transparente','15cm 125mm 40mm', 387.00,'1 ano', '/imagemProdutos/dolce124.jpg');
call pa_inserirProduto ('Oakley','Óculos de Grau', 'Ácrilico Redondo Feminino 35- Branco Normal Transparente', '14cm 115mm 45mm', 94.00,'1 ano', '/imagemProdutos/femoakley112.jpg');
call pa_inserirProduto ('Rayban','Óculos de Grau', 'Ácrilico Redondo Feminino 15-25 Preto Normal Transparente', '14cm 115mm 45mm', 94.00,'1 ano', '/imagemProdutos/femrayban298.jpg');
call pa_inserirProduto ('Chilli Beans','Óculos de Grau', 'Acetato Quadrado Feminino 35-45 Preto Normal Transparente','15cm 130mm 38mm', 500.00,'6 meses', '/imagemProdutos/hickman165.jpg');


-- OCULOS DE GRAU MASCULINO
call pa_inserirProduto ('Hip7','Óculos de Grau', 'Metal Aviador Masculino 35-45 Dourado Normal Transparente','15cm 130mm 40mm', 185.00,'6 meses', '/imagemProdutos/hip7Dourado185.jpg');
call pa_inserirProduto ('Rayban','Óculos de Grau', 'Metal Aviador Masculino 35-45 Cinza Normal Transparente','15cm 120mm 30mm', 464.00,'1 ano', '/imagemProdutos/rayban464.jpg');
call pa_inserirProduto ('Atitude','Óculos de Grau', 'Metal Quadrado Masculino 5-10 Azul Normal Transparente','10cm 120mm 30mm', 183.00,'1 ano', '/imagemProdutos/atitude183.jpg');
call pa_inserirProduto ('Hip7','Óculos de Grau', 'Metal Quadrado Masculino 35-45 Preto Normal Transparente','15cm 130mm 40mm', 62.20,'6 meses', '/imagemProdutos/grau6220.jpg');
call pa_inserirProduto ('Rayban','Óculos de Grau', 'Metal Aviador Masculino 35-45 Dourado Normal Transparente','15cm 120mm 30mm', 77.00,'1 ano', '/imagemProdutos/graumas77.jpg');
call pa_inserirProduto ('Atitude','Óculos de Grau', 'Acrílico Quadrado Masculino 35-45 Vermelho Normal Transparente','10cm 120mm 30mm', 310.00,'1 ano', '/imagemProdutos/graumas310.jpg');
call pa_inserirProduto ('Hip7','Óculos de Grau', 'Metal Quadrado Masculino 35-45 Vermelho Normal Transparente','15cm 130mm 40mm', 780.00,'6 meses', '/imagemProdutos/graumas780.jpg');
call pa_inserirProduto ('Rayban','Óculos de Grau', 'Metal Quadrado Masculino 35-45 Azul Normal Transparente','15cm 120mm 30mm', 65.78,'1 ano', '/imagemProdutos/graumas6578.jpg');
call pa_inserirProduto ('Atitude','Óculos de Grau', 'Acetato Redondo Masculino 5-10 Branco Normal Transparente','10cm 120mm 30mm', 50.00,'1 ano', '/imagemProdutos/graubranco.jpg');


-- LENTE MULTIFOCAL
call pa_inserirProduto ('Solotica','Lente', 'Ácrilico Multifocal', 'Antiabrasivo Fotosensível Antirreflexo ProteçãoUVA/UVB', 1000.00,'1 ano', '/imagemProdutos/lentes.jpg');
call pa_inserirProduto ('Crizal','Lente', 'Ácrilico Multifocal', 'Antirreflexo ProteçãoUVA/UVB', 400.00,'1 ano', '/imagemProdutos/lentes.jpg');
call pa_inserirProduto ('Acuvue','Lente', 'Policarbonato Multifocal', 'Antiabrasivo Fotosensível Antirreflexo', 320.00,'1 ano', '/imagemProdutos/lentes.jpg');

-- LENTE MONOFOCAL
call pa_inserirProduto ('Natural Vision','Lente', 'Crizal Monofocal', 'Antiabrasivo Antirreflexo ProteçãoUVA/UVB', 700.00,'1 ano', '/imagemProdutos/lentes.jpg');
call pa_inserirProduto ('Biofinty','Lente', 'Policarbonato Monofocal', 'Antiabrasivo Fotosensível', 250.00,'1 ano', '/imagemProdutos/lentes.jpg');
call pa_inserirProduto ('Acuvue','Lente', 'Policarbonato Monofocal', 'Antiabrasivo Fotosensível Antirreflexo', 320.00,'1 ano', '/imagemProdutos/lentes.jpg');

          











/* -- ESTOQUE*/ 
DELIMITER $$
drop procedure if exists pa_inserirEstoque $$
create Procedure pa_inserirEstoque (
$cd_produto int,
$qt_estoque int
)
main: begin
	insert into tbEstoque (
	cd_produto,
	qt_estoque
    )
	values (
    $cd_produto,
	$qt_estoque
    );
end$$
DELIMITER ;



call pa_inserirEstoque (1, 20);
call pa_inserirEstoque (2, 60);
call pa_inserirEstoque (3, 20);
call pa_inserirEstoque (4, 100);
call pa_inserirEstoque (5, 120);
call pa_inserirEstoque (6, 150);
call pa_inserirEstoque (7, 105);
call pa_inserirEstoque (8, 300);
call pa_inserirEstoque (9, 80);
call pa_inserirEstoque (10, 70);
call pa_inserirEstoque (11, 50);
call pa_inserirEstoque (12, 60);
call pa_inserirEstoque (13, 0);
call pa_inserirEstoque (14, 0);
call pa_inserirEstoque (15, 0);
call pa_inserirEstoque (16, 60);
call pa_inserirEstoque (17, 65);
call pa_inserirEstoque (18, 115);
call pa_inserirEstoque (19, 5);
call pa_inserirEstoque (20, 160);
call pa_inserirEstoque (21, 170);
call pa_inserirEstoque (22, 200);
call pa_inserirEstoque (23, 200);
call pa_inserirEstoque (24, 200);
call pa_inserirEstoque (25, 200);
call pa_inserirEstoque (26, 200);
call pa_inserirEstoque (27, 200);
call pa_inserirEstoque (28, 200);
call pa_inserirEstoque (29, 200);
call pa_inserirEstoque (30, 200);
call pa_inserirEstoque (31, 200);
call pa_inserirEstoque (32, 198);
call pa_inserirEstoque (33, 150);
call pa_inserirEstoque (34, 200);
call pa_inserirEstoque (35, 200);
call pa_inserirEstoque (36, 200);
call pa_inserirEstoque (37, 2);
call pa_inserirEstoque (38, 50);
call pa_inserirEstoque (39, 200);
call pa_inserirEstoque (40, 200);
call pa_inserirEstoque (41, 100);
call pa_inserirEstoque (42, 100);











/* -- PEDIDO*/ 
DELIMITER $$
drop procedure if exists pa_inserirPedido $$
create Procedure pa_inserirPedido(
$cd_cliente int,
$dt_pedido varchar(10)
)
main: begin
	insert into tbPedido (
    cd_cliente,
	dt_pedido 
    )
	values  (
    $cd_cliente,
    $dt_pedido 
	);
end$$
DELIMITER ;

call pa_inserirPedido (1, '20/08/2018');
call pa_inserirPedido (2, '05/09/2018');
call pa_inserirPedido (3, '06/05/2018');








/* -- PEDIDO ITENS*/ 
DELIMITER $$
drop procedure if exists pa_inserirPedidoItens $$
create Procedure pa_inserirPedidoItens(
	$cd_pedido int,
	$cd_produto int,
	$qt_item int
)
main :begin
    insert into tbPedidoItens (
    cd_pedido, cd_produto, qt_item, vl_subtotal
    )
    values (
    $cd_pedido, $cd_produto, $qt_item, (select vl_preco_unitario * $qt_item from tbProduto where cd_produto = $cd_produto)
    );
end$$
DELIMITER ;

call pa_inserirPedidoItens (1, 2, 3);
call pa_inserirPedidoItens (2, 3, 5);
call pa_inserirPedidoItens (3, 1, 5);






/* -- PEDIDO RECEITA*/ 
DELIMITER $$
drop procedure if exists pa_inserirPedidoReceita $$
create Procedure pa_inserirPedidoReceita($cd_pedido int, $cd_receita int)
main: begin
insert into tbPedidoReceita (cd_pedido, cd_receita)
values ($cd_pedido, $cd_receita);

end$$
DELIMITER ;

call pa_inserirPedidoReceita (1, 1);
call pa_inserirPedidoReceita (2, 2);
call pa_inserirPedidoReceita (3, 3);

-- call pa_exibirReceita;






/* -- PAGAMENTO*/ 
DELIMITER $$
drop procedure if exists pa_inserirFormaPagamento $$
create Procedure pa_inserirFormaPagamento(
	$cd_pedido int, 
	$tipo_pagamento varchar(255), 
	$parcelamento varchar(40)
)
main: begin
    insert into tbFormaPagamento (cd_pedido, tipo_pagamento, parcelamento, vl_total)
    values ($cd_pedido, $tipo_pagamento, $parcelamento, (select SUM(vl_subtotal)from tbPedidoItens where cd_pedido = $cd_pedido));
end$$
DELIMITER ;

call pa_inserirFormaPagamento(1, '32105.12368 32145.25418 12365.321458 5 4120000201503', 'Á VISTA');
call pa_inserirFormaPagamento (2, 'JORGE-CARNEIRO 0231.2253.2158.5986 VISA 521 05/2020', '3');
call pa_inserirFormaPagamento (3, 'MARIA-VELOSO 9752.4250.8542.1452 MASTERCARD 852 10/2022', 'Á VISTA');




/* -- NOTIFICACAO*/ 
DELIMITER $$
drop procedure if exists pa_inserirNotificacao $$
create Procedure pa_inserirNotificacao(
	$cd_cliente int,$cd_produto int
)
main: begin
	insert into tbProdutoNotificacao (cd_cliente, cd_produto)
	values ($cd_cliente, $cd_produto);
end$$
DELIMITER ;

call pa_inserirNotificacao (1, 1);
call pa_inserirNotificacao (2, 2);
call pa_inserirNotificacao (3, 3);




/* -- CARRINHO*/ 
DELIMITER $$
drop procedure if exists pa_inserirCarrinhoCompra $$

create Procedure  pa_inserirCarrinhoCompra (
	$cd_produto int, 
	$cd_cliente int,
	$qt_item int
)
main: begin
	insert into tbCarrinhoCompra (cd_produto, cd_cliente, qt_item)
	values ($cd_produto, $cd_cliente, $qt_item);
end$$
DELIMITER ;

call pa_inserirCarrinhoCompra (1,1,10);
call pa_inserirCarrinhoCompra (2,2,15);
call pa_inserirCarrinhoCompra (3,3,15);





-- CARRINHO RECEITA INSERIR----------------
DELIMITER $$
drop procedure if exists pa_inserirCarrinhoReceita $$

create Procedure pa_inserirCarrinhoReceita(
	$cd_cliente int,
    $cd_carrinho int,
    $cd_receita int
)
main: begin
	insert into tbCarrinhoReceita (cd_cliente, cd_carrinho, cd_receita)
	values ($cd_cliente, $cd_carrinho, $cd_receita);
end$$
DELIMITER ;


-- call pa_inserirCarrinhoReceita(1, 2, 1)
