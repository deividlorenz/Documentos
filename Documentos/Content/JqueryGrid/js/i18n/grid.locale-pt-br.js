;(function($){
/**
 * jqGrid Brazilian-Portuguese Translation
 * Sergio Righi sergio.righi@gmail.com
 * http://curve.com.br
 * Dual licensed under the MIT and GPL licenses:
 * http://www.opensource.org/licenses/mit-license.php
 * http://www.gnu.org/licenses/gpl.html
**/
$.jgrid = $.jgrid || {};
$.extend($.jgrid,{
	defaults : {
		recordtext: "Ver {0} - {1} of {2}",
	    emptyrecords: "Nenhum registro para visualizar",
		loadtext: "Carregando...",
		pgtext : "Página {0} de {1}"
	},
	search : {
      caption: "Procurar...",
	    Find: "Procurar",
	    Reset: "Reiniciar",
	    odata : ['igual', 'diferente', 'menor', 'menor ou igual','maior','maior ou igual', 'come&ccedil;a com','n&atilde;o come&ccedil;a com','est&aacute; em','n&atilde;o est&aacute; em','termina com','n&atilde;o termina com','cont&eacute;m','n&atilde;o cont&eacute;m'],
	    groupOps: [	{ op: "AND", text: "e" }, { op: "OR",  text: "ou" }	],
			matchText: " igual a",
			rulesText: " regras"
	},
	edit : {
	    addCaption: "Incluir",
	    editCaption: "Alterar",
	    bSubmit: "Enviar",
	    bCancel: "Cancelar",
		bClose: "Fechar",
		saveData: "Os dados foram alterados! Salvar alterações?",
		bYes : "Sim",
		bNo : "Não",
		bExit : "Cancelar",
	    msg: {
	        required:"Campo obrigatório",
	        number:"Por favor, informe um número válido",
	        minValue:"valor deve ser igual ou maior que ",
	        maxValue:"valor deve ser menor ou igual a",
	        email: "este e-mail não é válido",
	        integer: "Por favor, informe um valor inteiro",
			date: "Por favor, informe uma data válida",
			url: "não é uma URL válida. Prefixo obrigatório ('http://' or 'https://')",
			nodefined : " não está definido!",
			novalue : " um valor de retorno é obrigatório!",
			customarray : "Função customizada deve retornar um array!",
			customfcheck : "Função customizada deve estar presente em caso de validação customizada!"
		}
	},
	view : {
	    caption: "Ver Registro",
	    bClose: "Fechar"
	},
	del : {
    caption: "Apagar",
	    msg: "Apagar registros selecionado(s)?",
	    bSubmit: "Apagar",
	    bCancel: "Cancelar"
	},
	nav : {
		edittext: "<span class='footer_img'>&nbsp;<img src='Content/imagens/edit.png' alt='Editar' title='Editar' /></span>",
	    edittitle: "Editar",
		addtext:"<span class='footer_img'>&nbsp;<img src='Content/imagens/add.png' alt='Adicionar' title='Adicionar' /></span>",
	    addtitle: "Adicionar",
	    deltext: "<span class='footer_img'>&nbsp;<img src='Content/imagens/del.png' alt='Excluir' title='Excluir' /></span>",
	    deltitle: "Excluir",
	    searchtext: "<span class='footer_img'>&nbsp;<img src='Content/imagens/find.png' alt='Procurar' title='Procurar' /></span>",
	    searchtitle: "Procurar",
	    refreshtext: "<span class='footer_img'>&nbsp;<img src='Content/imagens/refresh.png' alt='Atualizar' title='Atualizar' /></span>",
	    refreshtitle: "Atualizar",
	    alertcap: "Aviso",
	    alerttext: "Por favor, selecione um registro",
		viewtext: "",
		viewtitle: "Ver linha selecionada"
	},
	col : {
	    caption: "Mostrar/Esconder Colunas",
	    bSubmit: "Enviar",
	    bCancel: "Cancelar"
	},
	errors : {
		errcap : "Erro",
		nourl : "Nenhuma URL defenida",
		norecords: "Sem registros para exibir",
	    model : "Comprimento de colNames <> colModel!"
	},
	formatter : {
		integer : {thousandsSeparator: " ", defaultValue: '0'},
		number : {decimalSeparator:",", thousandsSeparator: " ", decimalPlaces: 2, defaultValue: '0,00'},
		currency : {decimalSeparator:",", thousandsSeparator: ".", decimalPlaces: 2, prefix: "R$ ", suffix:"", defaultValue: '0,00'},
		date : {
			dayNames:   [
				"Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb",
				"Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado"
			],
			monthNames: [
				"Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez",
				"Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
			],
			AmPm : ["am","pm","AM","PM"],
			S: function (j) {return j < 11 || j > 13 ? ['º', 'º', 'º', 'º'][Math.min((j - 1) % 10, 3)] : 'º'},
			srcformat: 'Y-m-d',
			newformat: 'd/m/Y',
			masks : {
	            ISO8601Long:"Y-m-d H:i:s",
	            ISO8601Short:"Y-m-d",
	            ShortDate: "n/j/Y",
	            LongDate: "l, F d, Y",
	            FullDateTime: "l, F d, Y g:i:s A",
	            MonthDay: "F d",
	            ShortTime: "g:i A",
	            LongTime: "g:i:s A",
	            SortableDateTime: "Y-m-d\\TH:i:s",
	            UniversalSortableDateTime: "Y-m-d H:i:sO",
	            YearMonth: "F, Y"
	        },
	        reformatAfterEdit : false
		},
		baseLinkUrl: '',
		showAction: '',
	    target: '',
	    checkbox : {disabled:true},
		idName : 'id'
	}
});
})(jQuery);
