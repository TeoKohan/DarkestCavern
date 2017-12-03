public struct CharacterInputData {
	public CharacterAction characterAction;
	public PickaxeAction pickaxeAction;
	public float movement;

	public CharacterInputData(CharacterAction characterAction, PickaxeAction pickaxeAction, float movement) {

		this.characterAction = characterAction;
		this.pickaxeAction = pickaxeAction;
		this.movement = movement;
	}
}