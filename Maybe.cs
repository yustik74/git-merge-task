using System;
using System.Collections.Generic;

namespace Kontur.Courses.Git
{
	public class Maybe<T>
	{
		public static implicit operator Maybe<T>(T value)
		{
			return new Maybe<T>(value, null, true);
		}

		public static Maybe<T> FromError(string errorFormat, params object[] args)
		{
			return new Maybe<T>(default(T), string.Format(errorFormat, args), false);
		}

		private Maybe(T value, string error, bool hasValue)
		{
			this.value = value;
			Error = error;
			HasValue = hasValue;
		}

		public override string ToString()
		{
			return HasValue ? Value.ToString() : "Error: " + Error;
		}

		protected bool Equals(Maybe<T> other)
		{
			return EqualityComparer<T>.Default.Equals(Value, other.Value) && string.Equals(Error, other.Error) && HasValue.Equals(other.HasValue);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Maybe<T>)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = EqualityComparer<T>.Default.GetHashCode(Value);
				hashCode = (hashCode * 397) ^ (Error != null ? Error.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ HasValue.GetHashCode();
				return hashCode;
			}
		}

		public T Value => HasValue ? value : throw new InvalidOperationException("No value due to error: " + Error);
		private readonly T value;
		public readonly string Error;
		public readonly bool HasValue;
	}
}