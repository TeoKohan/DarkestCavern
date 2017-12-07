public struct Inventory {
	public Bag bag;
	public Helmet helmet;
	public Pickaxe pickaxe;

	public Inventory (Bag bag, Helmet helmet, Pickaxe pickaxe) {
		this.bag = bag;
		this.helmet = helmet;
		this.pickaxe = pickaxe;
	}
}