public struct CharacterInputData {
	public CharacterAction action;
	public float movement;

	public CharacterInputData(CharacterAction action, float movement) {

		this.action = action;
		this.movement = movement;
	}
}