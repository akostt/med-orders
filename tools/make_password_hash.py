#!/usr/bin/env python3
"""
Хеш пароля в формате ASP.NET Core Identity (PasswordHasher, версия V3).
Нужен для учётных записей в миграции и для ручного добавления пользователей в SQL.

Использование:
    python3 tools/make_password_hash.py "МойПароль1"
Вывод: строка для колонки PasswordHash.
"""
import base64
import hashlib
import os
import sys

ITERATIONS = 100000
SALT_LENGTH = 16
SUBKEY_LENGTH = 32
FORMAT_V3 = 0x01
PRF_HMAC_SHA256 = 1


def make_hash(password: str) -> str:
    salt = os.urandom(SALT_LENGTH)
    subkey = hashlib.pbkdf2_hmac("sha256", password.encode("utf-8"), salt, ITERATIONS, SUBKEY_LENGTH)
    blob = (
        bytes([FORMAT_V3])
        + PRF_HMAC_SHA256.to_bytes(4, "big")
        + ITERATIONS.to_bytes(4, "big")
        + SALT_LENGTH.to_bytes(4, "big")
        + salt
        + subkey
    )
    return base64.b64encode(blob).decode("ascii")


def verify(password: str, hashed: str) -> bool:
    """Проверка тем же алгоритмом, что и Identity. Нужна для самопроверки скрипта."""
    blob = base64.b64decode(hashed)
    if blob[0] != FORMAT_V3:
        return False
    iterations = int.from_bytes(blob[5:9], "big")
    salt_length = int.from_bytes(blob[9:13], "big")
    salt = blob[13:13 + salt_length]
    subkey = blob[13 + salt_length:]
    candidate = hashlib.pbkdf2_hmac("sha256", password.encode("utf-8"), salt, iterations, len(subkey))
    return candidate == subkey


if __name__ == "__main__":
    if len(sys.argv) != 2:
        sys.exit("Использование: python3 tools/make_password_hash.py \"пароль\"")
    password = sys.argv[1]
    result = make_hash(password)
    if not verify(password, result):
        sys.exit("Самопроверка хеша не пройдена")
    print(result)
