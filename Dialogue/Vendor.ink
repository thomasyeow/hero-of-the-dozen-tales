->main
=== main ===
Hi, want to buy anything?
+[Yeah]
    ->shop
+[No]
    ->noShop

=== shop ===
Pleasure doing business with you.#shop
+[Likewise]
->END

=== noShop ===
Well, come back when you change your mind.
+[Ok]
->END

