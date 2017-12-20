public class Inventory {
	public Bag bag;
	public Helmet helmet;
	public Pickaxe pickaxe;

	public Inventory() {
		this.bag = new Bag();
		this.helmet = new Helmet();
		this.pickaxe = new Pickaxe();
	}

	public Inventory (Bag bag, Helmet helmet, Pickaxe pickaxe) {
		this.bag = bag;
		this.helmet = helmet;
		this.pickaxe = pickaxe;
	}
}