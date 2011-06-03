function Module() {
	Module.$parent.call(this);
}
Module.prototype.render = function(writer) { };
extend(Loadable, Module);