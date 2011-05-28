function InlineStrategy() {
	InlineStrategy.$parent.call(this);
}

extend(ScopeDrivenStrategy, InlineStrategy);