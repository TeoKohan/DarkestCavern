class Helper {
	public static T[] populate<T> (T[] array, T value ) {
		for ( int i = 0; i < array.Length; i++ ) {
			array[i] = value;
		}

		return array;
	}
}