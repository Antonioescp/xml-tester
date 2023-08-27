# xml-tester

Aplicacion de consola para probar la transformacion de archivos xml

# Uso

### Archivos con mismo nombre

Si se sigue la convención de tener un `<archivo>` y su respectivo archivo de transformaciones `<archivo>.xmgr`, se puede utilizar la opción `-s <archivo>`, el programa asume que el archivo de transformación se llama `<archivo>.xmgr`.

#### Ejemplo

Teniendo el arbol de archivos

```
./
|- archivo.xml
|- archivo.xml.xmgr
```

se puede usar el comando

```
xml-tester -s archivo.xml
```

El arbol resultante sería

```
./
|- archivo.xml
|- archivo.xml.xmgr
|- archivo-transformado.xml
```

### Archivos con distinto nombre

Se puede especificar el archivo de entrada con `-i <archivo>`, el archivo de transformación con `-t <archivo>` y el archivo de salida con `-o <archivo>`.

#### Ejemplo

Teniendo el arbol de archivos

```
./
|- archivo.xml
|- archivo.xmgr
```

se puede usar el comando

```
xml-tester -i archivo.xml -t archivo.xmgr -o archivo-transformado.xml
```

El arbol resultante sería

```
./
|- archivo.xml
|- archivo.xmgr
|- archivo-transformado.xml
```
