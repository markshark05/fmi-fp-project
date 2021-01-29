; bitwise functions
(define (bit-shift-right n k)
  (quotient n (expt 2 k))
  )

(define (bit-shift-left n k)
  (* n (expt 2 k))
  )

(define (bit-get n k)
  (remainder (bit-shift-right n k) 2)
  )

(define (to-bit p) (if p 1 0))

(define (bit-count a)
  (define (loop c a)
    (if (> a 0)
        (loop (+ c (bit-get a 0)) (bit-shift-right a 1))
        c
        )
    )
  (loop 0 a)
  )

(define (bit-accumulate a b op)
  (define (loop init i a b)
    (if (or (> a 0) (> b 0))
        (loop
         (+ init (bit-shift-left (op (bit-get a 0) (bit-get b 0)) i))
         (+ i 1)
         (bit-shift-right a 1)
         (bit-shift-right b 1)
         )
        init
        )
    )
  (loop 0 0 a b)
  )

(define (bit-and a b)
  (define (op a b) (to-bit (and (= a 1) (= b 1))))
  (bit-accumulate a b op)
  )

(define (bit-or a b)
  (define (op a b) (to-bit (or (= a 1) (= b 1))))
  (bit-accumulate a b op)
  )

(define (bit-except a b)
  (define (op a b) (to-bit (and (= a 1) (= b 0))))
  (bit-accumulate a b op)
  )


; set functions
(define (set-singleton elem)
  (bit-shift-left 1 elem)
  )

(define (set-union s1 s2)
  (bit-or s1 s2)
  )

(define (set-add set elem)
  (set-union set (set-singleton elem))
  )

(define (set-difference s1 s2)
  (bit-except s1 s2)
  )

(define (set-remove set elem)
  (set-difference set (set-singleton elem))
  )

(define (set-empty? set)
  (= set 0)
  )

(define (set-intersect s1 s2)
  (bit-and s1 s2)
  )

(define (set-contains? set elem)
  (not (set-empty? (set-intersect set (set-singleton elem))))
  )
  
(define (set-size set)
  (bit-count set)
  )

; set functions examples
(set-add (set-add (set-add 0 5) 1) 0) ; 35
(set-add (set-add (set-add 0 4) 3) 1) ; 26
(set-add 26 4)                        ; 26
(set-size 26)                         ; 3
(set-remove 26 4)                     ; 10
(set-contains? 26 4)                  ; #t

