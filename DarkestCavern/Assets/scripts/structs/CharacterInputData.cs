public struct CharacterInputData {
	public CharacterAction characterAction;
	public float movement;

	public CharacterInputData(CharacterAction characterAction, float movement) {

		this.characterAction = characterAction;
		this.movement = movement;
	}
}