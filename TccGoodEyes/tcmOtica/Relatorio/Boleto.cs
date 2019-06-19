using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Xml.Linq;
using System.IO;
using tccGoodEyes.Models;
using System.Text;
using System.Web.Hosting;

namespace tccGoodEyes.Relatorio
{
    public class Boleto
    {


        #region Declaration
        int colunasTotal = 6;
        Document doc;
        Font fontStyle;
        PdfPTable pdfTabela = new PdfPTable(6);
        PdfPCell pdfCell;
        MemoryStream memory = new MemoryStream();
        List<VendasModel> listVendas = new List<VendasModel>();

        string CODIGO_BANCO = "104";
        string CODIGO_AGENCIA = "0877";
        string CODIGO_BENEFICIARIO = "3479710";
        string MOEDA = "9";
        string CPF_CNPJ = "14.796.606/0001-90";

        int digitoVerificador;
        double valor;
        string numDoc, nossoNumero, nomeCli, cpf, endereco;
        string campo1, campo2, campo3, campo4, campo5;
        string linhaDigitavel;
        #endregion

        private int geraAleatorio(int max)
        {
            Random random = new Random();
            return random.Next(max);
        }

        private int geraNumDocumento()
        {
            Random random = new Random();
            return random.Next(100000000,1000000000);
        }

        public string gerarNumeroBoleto(double valorTotal)
        {
            numDoc = geraAleatorio(10000000).ToString();
            nossoNumero = geraNumDocumento().ToString();
            digitoVerificador = geraAleatorio(10);
            valor = valorTotal;

            campo1 = CODIGO_BANCO + MOEDA + geraAleatorio(10) + "." + CODIGO_BENEFICIARIO.Substring(0, 3) + digitoVerificador;
            campo2 = " " + CODIGO_BENEFICIARIO.Substring(3, 4) + geraAleatorio(10) + "." + nossoNumero.Substring(0, 5) + digitoVerificador;
            campo3 = " " + nossoNumero.Substring(5, 4) + "." + geraAleatorio(4000) + digitoVerificador;
            campo4 = " " + digitoVerificador + " ";
            campo5 = DateTime.Now.ToString("ddMMyyyy") + 0000 + valor.ToString().Trim('.');

            linhaDigitavel = campo1 + campo2 + campo3 + campo4 + campo5;

            return linhaDigitavel;
        }

        public byte[] GeraBoleto(ClienteModel dto, string linhaDigi, double valorTotal)
        {
            valor = valorTotal;
            nomeCli = dto.nome;
            cpf = dto.no_cpf;
            endereco = dto.nm_rua + "," + dto.no_cpf + " " + dto.bairro + " " + dto.cidade + " - " + dto.sg_uf;
            linhaDigitavel = linhaDigi;

            #region
            doc = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            doc.SetPageSize(PageSize.A4);
            doc.SetMargins(20f, 20f, 20f, 20f);
            pdfTabela.WidthPercentage = 100;
            pdfTabela.HorizontalAlignment = Element.ALIGN_LEFT;
            fontStyle = FontFactory.GetFont("Calibri");
            PdfWriter.GetInstance(doc, memory);
            doc.Open();
            pdfTabela.SetWidths(new float[] { 50f, 50f, 50f, 50f, 50f, 50f });
            #endregion

            //primeira linha 
            fontStyle = FontFactory.GetFont("Tahoma", 12f, 1);
            pdfCell = new PdfPCell(new Phrase("RECIBO DO PAGADOR", fontStyle));
            pdfCell.Colspan = colunasTotal;
            pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfCell.Padding = 5;
            pdfCell.Border = 0;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);

            pdfTabela.CompleteRow();

            BoletoTopo();
            ReciboDoPagador();
            
            BoletoTopo();
            BoletoCorpo();


            pdfTabela.HeaderRows = 2;
            doc.Add(pdfTabela);
            doc.Close();
            return memory.ToArray();

        }

        //TOPO DA PÁGINA
        public void BoletoTopo()
        {
            // PRIMEIRA LINHA DA TABELA/DO BOLETO
            //LOGO DA CAIXA
            var img = Image.GetInstance(HostingEnvironment.ApplicationPhysicalPath.ToString().Replace('\\', '/') + "imagens/caixaLogo.jpg");
            pdfCell = new PdfPCell();
            pdfCell.AddElement(img);
            pdfCell.Colspan = 1;
            pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.HorizontalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.Padding = 5;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);

            //CODIGO DO BANCO
            fontStyle = FontFactory.GetFont("Tahoma", 18f, 1);
            pdfCell = new PdfPCell(new Phrase(CODIGO_BANCO + "-0", fontStyle));
            pdfCell.Colspan = 1;
            pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.HorizontalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.Padding = 5;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);

            //LINHA DIGITÁVEL QUE POSSIBILITA PAGAR ONLINE O BOLETO
            fontStyle = FontFactory.GetFont("Tahoma", 12f, 1);
            pdfCell = new PdfPCell(new Phrase(linhaDigitavel, fontStyle));
            pdfCell.Colspan = 4;
            pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.HorizontalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.Padding = 5;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);

            pdfTabela.CompleteRow();
            
        }

        private static string AppendAtPosition(string baseString, int position, string character)
        {
            var sb = new StringBuilder(baseString);
            for (int i = position; i < sb.Length; i += (position + character.Length))
                sb.Insert(i, character);
            return sb.ToString();
        }

        public void celulaTitulo(string Titulo, int colspan)
        {
            
            fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            pdfCell = new PdfPCell(new Phrase(Titulo, fontStyle));
            pdfCell.Colspan = colspan;
            pdfCell.HorizontalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.Padding = 5;
            pdfCell.BorderWidthBottom = 0;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);
        }

        public void celulaValor(string valor, int colspan)
        {

            fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);
            pdfCell = new PdfPCell(new Phrase(valor, fontStyle));
            pdfCell.Colspan = colspan;
            pdfCell.HorizontalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.Padding = 5;
            pdfCell.BorderWidthTop = 0;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);
        }

        public void ReciboDoPagador()
        {
            //-------------------------------
            //TITULOS
            //
            //BENEFICIARIO
            celulaTitulo("Beneficiário", 3);

            //COD AGENCIA E COD BENEFICIARIO
            celulaTitulo("Agência / Código do Beneficiário", 1);

            //ESPECIE
            celulaTitulo("Espécie", 1);

            //NOSSO NÚMERO
            celulaTitulo("Nosso Número", 1);

            pdfTabela.CompleteRow();

            //-------------------------------
            //CONTEUDO
            //
            //BENEFICIARIO
            celulaValor("ÓTICA GOOD EYES COMERCIO DE PRODUTOS OPTICOS", 3);

            //COD AGENCIA E COD BENEFICIARIO
            celulaValor(CODIGO_AGENCIA + "/" + CODIGO_BENEFICIARIO, 1);

            //ESPECIE
            celulaValor("REAL", 1);

            //NOSSO NÚMERO
            celulaValor(nossoNumero, 1);

            pdfTabela.CompleteRow();

            //-------------------------------
            //TITULO
            //
            celulaTitulo("CPF / CNPJ",2);

            celulaTitulo("Data de Vencimento", 2);

            celulaTitulo("Valor do Documento", 2);
            pdfTabela.CompleteRow();

            //-------------------------------
            //CONTEUDO
            //
            //CPF-CNPJ
            celulaValor(CPF_CNPJ, 2);

            //DATA DE VENCIMENTO
            celulaValor(DateTime.Now.AddDays(5).ToString("dd/MM/yyyy"), 2);


            //VALOR DO DOCUMENTO
            celulaValor(valor.ToString(), 2);

            pdfTabela.CompleteRow();

            //-------------------------------
            //TITULO
            //
            celulaTitulo("Pagador", 6);

            pdfTabela.CompleteRow();

            //-------------------------------
            //CONTEUDO
            //
            //PAGADOR
            celulaValor(nomeCli + ", CPF: " + cpf, 6);
            
            pdfTabela.CompleteRow();

            //-------------------------------
            //TITULO
            //
            celulaTitulo("Instruções", 5);

            celulaTitulo("Autentificação Mecânica", 1);

            pdfTabela.CompleteRow();

            //-------------------------------
            //CONTEUDO
            //

            //INSTRUÇÕES
            celulaValor("Não aceitar pagamento via cheque e/ou após a data de vencimento.", 5);

            //AUTENTIFICAÇÃO MECANICA
            celulaValor(" ", 1);

            pdfTabela.CompleteRow();


            //LINHA DE CORTE
            var img = Image.GetInstance(HostingEnvironment.ApplicationPhysicalPath.ToString().Replace('\\', '/') + "imagens/linhaCorte.png");
            pdfCell = new PdfPCell();
            pdfCell.AddElement(img);
            pdfCell.Colspan = 6;
            pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.HorizontalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.Padding = 5;
            pdfCell.Border = 0;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);
            pdfTabela.CompleteRow();
        }

        //CORPO DO RELATÓRIO
        public void BoletoCorpo()
        {
            //-------------------------------
            //TITULO
            //
            celulaTitulo("Local de Pagamento", 5);

            celulaTitulo("Vencimento", 1);

            pdfTabela.CompleteRow();

            //-------------------------------
            //CONTEUDO
            //

            //LOCAL DE PAGAMENTO
            celulaValor("Pagável em qualquer banco até o vencimento", 5);

            //DATA DE VENCIMENTO
            celulaValor(DateTime.Now.AddDays(5).ToString("dd/MM/yyyy"), 2);

            pdfTabela.CompleteRow();

            //-------------------------------
            //TITULO
            //
            celulaTitulo("Beneficiário", 3);

            celulaTitulo("Agência / Código do Beneficiário",3);

            pdfTabela.CompleteRow();

            //-------------------------------
            //CONTEUDO
            //
            //BENEFICIARIO
            celulaValor("ÓTICA GOOD EYES COMERCIO DE PRODUTOS OPTICOS", 3);

            //COD AGENCIA E COD BENEFICIARIO
            celulaValor(CODIGO_AGENCIA + "/" + CODIGO_BENEFICIARIO, 3);

            pdfTabela.CompleteRow();


            //-------------------------------
            //TITULO
            //
            celulaTitulo("Data do Documento", 1);

            celulaTitulo("Nº do documento", 1);

            celulaTitulo("Espécie Doc.", 1);

            celulaTitulo("Aceite", 1);

            celulaTitulo("Data de Processamento", 1);

            celulaTitulo("Nosso Numero", 1);

            pdfTabela.CompleteRow();

            //-------------------------------
            //CONTEUDO
            //
            //Data
            celulaValor(DateTime.Now.ToString("dd/MM/yyyy"), 1);

            //num
            celulaValor(numDoc, 1);

            //num
            celulaValor("DM", 1);

            //num
            celulaValor("N", 1);

            //Data
            celulaValor(DateTime.Now.ToString("dd/MM/yyyy"), 1);

            //num
            celulaValor(nossoNumero, 1);

            pdfTabela.CompleteRow();


            //-------------------------------
            //TITULO
            //
            celulaTitulo("Uso do Banco", 1);

            celulaTitulo("Carteira", 1);

            celulaTitulo("Espécie", 1);

            celulaTitulo("Quantidade", 1);

            celulaTitulo("Valor", 1);

            celulaTitulo("Valor Documento", 1);

            pdfTabela.CompleteRow();

            //-------------------------------
            //CONTEUDO
            //
            //Data
            celulaValor(" ", 1);

            //num
            celulaValor("101", 1);

            //num
            celulaValor("REAL", 1);

            //num
            celulaValor(" ", 1);

            //Data
            celulaValor(" ", 1);

            //num
            celulaValor(valor.ToString(), 1);

            pdfTabela.CompleteRow();

            //-------------------------------
            //TITULO
            //
            celulaTitulo("Instruções (texto de responsabilidade do Beneficiário)", 6);

            //-------------------------------
            //CONTEUDO
            //
            celulaValor("Não aceitar pagamento via cheque e/ou após a data do vencimento." +
                "Seu pedido será enviado somente após a confirmação do pagamento deste boleto, desde que" +
                "não tenha divergência de valores entre o valor cobrado e o valor pago." +
                "A falta de pagamento deste boleto não implica em qualquer multa ou juros e o pedido será automaticamente cancelado." +
                "Não deposite nem faça transferência.", 6);

            pdfTabela.CompleteRow();

            //-------------------------------
            //TITULO
            //
            celulaTitulo("Pagador", 6);

            //-------------------------------
            //CONTEUDO
            //
            celulaValor(nomeCli + ", CPF: " + cpf + " " + endereco, 6);

            pdfTabela.CompleteRow();


            //CÓDIGO DE BARRAS
            var img = Image.GetInstance(HostingEnvironment.ApplicationPhysicalPath.ToString().Replace('\\', '/') + "imagens/codigoDeBarras.png");
            pdfCell = new PdfPCell();
            pdfCell.AddElement(img);
            pdfCell.Colspan = 4;
            pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.HorizontalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.Padding = 5;
            pdfCell.Border = 0;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);

            
            fontStyle = FontFactory.GetFont("Tahoma", 5f, 0);
            pdfCell = new PdfPCell(new Phrase("Autentificação Mecânica", fontStyle));
            pdfCell.Colspan = 1;
            pdfCell.HorizontalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.Padding = 5;
            pdfCell.Border = 0;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);

          

            fontStyle = FontFactory.GetFont("Tahoma", 8f, 0);
            pdfCell = new PdfPCell(new Phrase("FICHA DE COMPENSAÇÃO", fontStyle));
            pdfCell.Colspan = 1;
            pdfCell.HorizontalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.Padding = 5;
            pdfCell.Border = 0;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);

            pdfTabela.CompleteRow();
        }

    }
}