ddaccordion.init({
	headerclass: "submenuheader",
	contentclass: "submenu",
	revealtype: "click",
	mouseoverdelay: 200,
	collapseprev: true,
	defaultexpanded: [],
	onemustopen: false,
	animatedefault: false,
	persiststate: true,
	toggleclass: ["", ""],
	togglehtml: ["suffix", "<img src='../Images/plusACM.gif' class='statusicon' />", "<img src='../Images/minusACM.gif' class='statusicon' />"],

	animatespeed: "fast",
	oninit: function (headers, expandedindices) { 
        //custom code to run when headers have initalized
	},
	onopenclose: function (header, index, state, isuseractivated) { 
        //custom code to run whenever a header is opened or closed
	}
})