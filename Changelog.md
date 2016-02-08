# v1.1 #

  * Added packet.dll for hooking packets.
  * Fixed a lot of bugs, including the full light.
  * Fixed PokemonAPI\_Inject (removed some parts,  we re-add soom as possible).
  * Fixed turn packet, now its called using the packet.dll.
  * Fixed move packet, now its called using the packet.dll.
  * Removed Icons, Skins and ContextMenus packets, we need to check what of this is crashing client at start up. We add again.
  * Added more functions to Battlelist class.
  * Corrected slot numbers.
  * Replaced Mana, ManaMax to Pokemons, PokemonsMax.
  * Replaced Capacity to PokemonsCount
  * Removed non 8.1 functions and classes.

# v1.0 #

  * Initial release
  * Corrected some addresses of TibiaApi to work with PXG client.
  * Corrected client chooser to work with the PXG client.