Shader "Masked/DepthMask" {
 
	// Done by Gabriel Blackwell
	// Found code on the web abotu this from WaybackMachine, using only draw to the zBuffer

	SubShader {
		// Render the mask after regular geometry, but before masked geometry and
		// transparent things.
 
		Tags {"Queue" = "Geometry+10" }
 
		// Don't draw in the RGBA channels; just the depth buffer
 
		ColorMask 0
		ZWrite On
 
		// Do nothing specific in the pass:
 
		Pass {}
	}
}